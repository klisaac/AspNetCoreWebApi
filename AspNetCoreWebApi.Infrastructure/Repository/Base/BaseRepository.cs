
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspNetCoreWebApi.Core.Repository.Base;
using AspNetCoreWebApi.Core.Entities.Base;
using AspNetCoreWebApi.Core.Specifications.Base;
using AspNetCoreWebApi.Infrastructure.Data;
using AspNetCoreWebApi.Core.Specifications;

namespace AspNetCoreWebApi.Infrastructure.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : AuditEntity
    {
        protected readonly AppDataContext _AspNetCoreWebApiContext;
        private DbSet<T> _entities;
        public BaseRepository(AppDataContext AspNetCoreWebApiContext)
        {
            _AspNetCoreWebApiContext = AspNetCoreWebApiContext ?? throw new ArgumentNullException(nameof(AspNetCoreWebApiContext));
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _AspNetCoreWebApiContext.Set<T>();

                return _entities;
            }
        }

        public IQueryable<T> Table => Entities.Where(t => t.IsDeleted == false);

        public IQueryable<T> TableNoTracking => Entities.Where(t => t.IsDeleted == false).AsNoTracking();

        public async virtual Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await Entities.Where(t => t.IsDeleted == false).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _AspNetCoreWebApiContext.Set<T>().Where(t => t.IsDeleted == false).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllByIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _AspNetCoreWebApiContext.Set<T>();
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return await query.Where(c => c.IsDeleted == false).ToListAsync();
        }
        

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_AspNetCoreWebApiContext.Set<T>().Where(t => t.IsDeleted == false).AsQueryable(), spec);
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _AspNetCoreWebApiContext.Set<T>().Where(predicate.And(t => t.IsDeleted==false)).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _AspNetCoreWebApiContext.Set<T>().Where(t => t.IsDeleted == false);
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _AspNetCoreWebApiContext.Set<T>().Where(t => t.IsDeleted == false);
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _AspNetCoreWebApiContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetSingleAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).Where(t=> t.IsDeleted == false).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            _AspNetCoreWebApiContext.Entry(entities).State = EntityState.Added;
            await Entities.AddRangeAsync(entities);
            await _AspNetCoreWebApiContext.SaveChangesAsync();
            return entities;
        }

        public async Task<T> AddAsync(T entity)
        {
            _AspNetCoreWebApiContext.ChangeTracker.LazyLoadingEnabled = true;
            _AspNetCoreWebApiContext.Entry(entity).State = EntityState.Added;
            await _AspNetCoreWebApiContext.Set<T>().AddAsync(entity);
            await _AspNetCoreWebApiContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> AddByLoadingReferenceAsync(T entity, params Expression<Func<T, object>>[] loadProperties)
        {
            _AspNetCoreWebApiContext.Entry(entity).State = EntityState.Added;
            foreach (var loadProperty in loadProperties)
                _AspNetCoreWebApiContext.Entry(entity).Reference(loadProperty).Load();

            await _AspNetCoreWebApiContext.Set<T>().AddAsync(entity);
            await _AspNetCoreWebApiContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> AddByLoadingCollectionAsync(T entity, params Expression<Func<T, IEnumerable<object>>>[] loadProperties)
        {
            _AspNetCoreWebApiContext.Entry(entity).State = EntityState.Added;
            foreach (var loadProperty in loadProperties)
                _AspNetCoreWebApiContext.Entry(entity).Collection(loadProperty).Load();

            await _AspNetCoreWebApiContext.Set<T>().AddAsync(entity);
            await _AspNetCoreWebApiContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Detached;
            _AspNetCoreWebApiContext.Entry(entity).State = EntityState.Modified;
            await _AspNetCoreWebApiContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task<T> UpdateByLoadingReferenceAsync(T entity, params Expression<Func<T, object>>[] loadProperties)
        {
            _AspNetCoreWebApiContext.Entry(entity).State = EntityState.Modified;
            foreach (var loadProperty in loadProperties)
                _AspNetCoreWebApiContext.Entry(entity).Reference(loadProperty).Load();

            await _AspNetCoreWebApiContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _AspNetCoreWebApiContext.Set<T>().Remove(entity);
            await _AspNetCoreWebApiContext.SaveChangesAsync();
        }

        //public async Task SaveChangesAsync()
        //{
        //    await _AspNetCoreWebApiContext.SaveChangesAsync();
        //}
    }
}
