using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspNetCoreWebApi.Core.Entities.Base;
using AspNetCoreWebApi.Core.Specifications.Base;

namespace AspNetCoreWebApi.Core.Repository.Base
{
    public interface IBaseRepository<T> where T : AuditEntity
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllByIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<T> GetSingleAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<T> AddAsync(T entity);
        Task<T> AddByLoadingReferenceAsync(T entity, params Expression<Func<T, object>>[] loadProperties);
        Task<T> AddByLoadingCollectionAsync(T entity, params Expression<Func<T, IEnumerable<object>>>[] loadProperties);
        Task<T> UpdateAsync(T entity);
        Task<T> UpdateByLoadingReferenceAsync(T entity, params Expression<Func<T, object>>[] loadProperties);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
        //Task SaveChangesAsync();
    }
}
