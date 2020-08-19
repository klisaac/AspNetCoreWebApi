using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Entities.Base;

namespace AspNetCoreWebApi.Infrastructure.Data
{
    public class AppDataContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;
        //private IConfiguration _configuration;
        //private readonly ICurrentUser _currentUser;

        public AppDataContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        //public AppDataContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //public AppDataContext(DbContextOptions<AppDataContext> options, ICurrentUser currentUser)
        //    : base(options)
        //{
        //    _currentUser = currentUser;
        //}
        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractItem> ContractItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentItem> PaymentItems { get; set; }
        public DbSet<ContractPaymentAssociation> ContractPaymentAssociations { get; set; }
        public DbSet<ProductSpecificationAssociation> ProductSpecificationAssociations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderPaymentAssociation> OrderPaymentAssociations { get; set; }
        public DbSet<User> Users { get; set; }
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            //options.UseSqlServer(_configuration.GetConnectionString(Constants.DbConnectionStringKey));
            //options.UseSqlServer("Server=localhost;User Id=sa;password=Dev@2019;Database=AspNetCoreWebApiDev");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(AppModelBuilder.ConfigureAddress);
            modelBuilder.Entity<Category>(AppModelBuilder.ConfigureCategory);
            modelBuilder.Entity<Product>(AppModelBuilder.ConfigureProduct);
            modelBuilder.Entity<Specification>(AppModelBuilder.ConfigureSpecification);
            modelBuilder.Entity<Customer>(AppModelBuilder.ConfigureCustomer);
            modelBuilder.Entity<Contract>(AppModelBuilder.ConfigureContract);
            modelBuilder.Entity<ContractItem>(AppModelBuilder.ConfigureContractItem);
            modelBuilder.Entity<Payment>(AppModelBuilder.ConfigurePayment);
            modelBuilder.Entity<PaymentItem>(AppModelBuilder.ConfigurePaymentItem);
            modelBuilder.Entity<ContractPaymentAssociation>(AppModelBuilder.ConfigureContractPaymentAssociation);
            modelBuilder.Entity<ProductSpecificationAssociation>(AppModelBuilder.ConfigureProductSpecificationAssociation);
            modelBuilder.Entity<Order>(AppModelBuilder.ConfigureOrder);
            modelBuilder.Entity<OrderItem>(AppModelBuilder.ConfigureOrderItem);
            modelBuilder.Entity<OrderPaymentAssociation>(AppModelBuilder.ConfigureOrderPaymentAssociation);
            modelBuilder.Entity<User>(AppModelBuilder.ConfigureUser);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.IsDeleted = false;
                        //entry.Entity.CreatedBy = _currentUser.UserName;
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUser.UserName;
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction = _currentTransaction ?? await Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                //await SaveChangesAsync();
                await _currentTransaction?.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

    }
}
