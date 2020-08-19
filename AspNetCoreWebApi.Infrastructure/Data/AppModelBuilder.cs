using AspNetCoreWebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreWebApi.Infrastructure.Data
{
    public static class AppModelBuilder
    {
        public static void ConfigureAddress(EntityTypeBuilder<Address> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Address").HasKey(a => a.AddressId);
            entityTypeBuilder.Property(a => a.AddressId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(a => a.AddressType).IsRequired().HasMaxLength(100);
            entityTypeBuilder.Property(a => a.AddressLine).IsRequired(true).HasMaxLength(100);
            entityTypeBuilder.Property(a => a.Country).IsRequired(true).HasMaxLength(100);
            entityTypeBuilder.Property(a => a.City).IsRequired(false).HasMaxLength(100);
            entityTypeBuilder.Property(a => a.State).IsRequired(false).HasMaxLength(100);
            entityTypeBuilder.Property(a => a.ZipCode).IsRequired(true).HasMaxLength(100);
            entityTypeBuilder.Property(a => a.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(a => a.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(a => a.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(a => a.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(a => a.LastModifiedDate).IsRequired(false);
        }
        public static void ConfigureCategory(EntityTypeBuilder<Category> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Category").HasKey(c => c.CategoryId);
            entityTypeBuilder.Property(c => c.CategoryId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entityTypeBuilder.Property(c => c.Description).HasMaxLength(100);
            entityTypeBuilder.Property(c => c.ImageName).IsRequired(false).HasMaxLength(200);
            entityTypeBuilder.Property(c => c.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(c => c.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(c => c.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(c => c.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(c => c.LastModifiedDate).IsRequired(false);
        }
        public static void ConfigureContract(EntityTypeBuilder<Contract> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Contract").HasKey(c => c.ContractId);
            entityTypeBuilder.Property(c => c.ContractId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(c => c.CustomerId).IsRequired(true);
            entityTypeBuilder.Property(c => c.BillingAddressId);
            entityTypeBuilder.Property(c => c.ShippingAddressId);
            entityTypeBuilder.Property(c => c.Status);
            entityTypeBuilder.Property(c => c.GrandTotal).HasColumnType("decimal(16, 3)").IsRequired();
            entityTypeBuilder.Property(c => c.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(c => c.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(c => c.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(c => c.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(c => c.LastModifiedDate).IsRequired(false);
            entityTypeBuilder.HasOne(o => o.ShippingAddress).WithMany().OnDelete(DeleteBehavior.Restrict);
            entityTypeBuilder.HasOne(o => o.BillingAddress).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
        public static void ConfigureContractItem(EntityTypeBuilder<ContractItem> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("ContractItem").HasKey(ci => ci.ContractItemId);
            entityTypeBuilder.Property(ci => ci.ContractItemId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(ci => ci.Quantity).IsRequired(true);
            entityTypeBuilder.Property(ci => ci.UnitPrice).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(ci => ci.TotalPrice).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(ci => ci.ProductId);
            entityTypeBuilder.Property(ci => ci.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(ci => ci.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(ci => ci.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(ci => ci.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(ci => ci.LastModifiedDate).IsRequired(false);
        }
        public static void ConfigureContractPaymentAssociation(EntityTypeBuilder<ContractPaymentAssociation> entityTypeBuilder)
        {
            //entityTypeBuilder.ToTable("ContractPaymentAssociation").HasKey(cpa => cpa.ContractPaymentAssociationId);
            entityTypeBuilder.ToTable("ContractPaymentAssociation").Property(cpa => cpa.ContractPaymentAssociationId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(cpa => cpa.ContractId);
            entityTypeBuilder.Property(cpa => cpa.PaymentId);
            entityTypeBuilder.Property(cpa => cpa.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(cpa => cpa.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(cpa => cpa.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(cpa => cpa.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(cpa => cpa.LastModifiedDate).IsRequired(false);
            entityTypeBuilder.HasKey(cpa => new { cpa.ContractId, cpa.PaymentId });
            entityTypeBuilder.HasOne(cpa => cpa.Contract).WithMany(c => c.Payments).HasForeignKey(cpa => cpa.ContractId).OnDelete(DeleteBehavior.Restrict);
        }
        public static void ConfigureCustomer(EntityTypeBuilder<Customer> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Customer").HasKey(c=>c.CustomerId);
            entityTypeBuilder.Property(c=>c.CustomerId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(c=>c.Name).HasMaxLength(200);
            entityTypeBuilder.Property(c=>c.Surname).HasMaxLength(100);
            entityTypeBuilder.Property(c=>c.Phone).HasMaxLength(20);
            entityTypeBuilder.Property(c=>c.DefaultAddressId);
            entityTypeBuilder.Property(c=>c.Email).HasMaxLength(100);
            entityTypeBuilder.Property(c=>c.CitizenId).HasMaxLength(100);
            entityTypeBuilder.Property(c=>c.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(c=>c.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(c => c.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(c=>c.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(c=>c.LastModifiedDate).IsRequired(false);
        }
        public static void ConfigureOrder(EntityTypeBuilder<Order> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Order").HasKey(o => o.OrderId);
            entityTypeBuilder.Property(o => o.OrderId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(o => o.CustomerId).IsRequired();
            entityTypeBuilder.Property(o => o.BillingAddressId);
            entityTypeBuilder.Property(o => o.ShippingAddressId);
            entityTypeBuilder.Property(o => o.Status);
            entityTypeBuilder.Property(o => o.GrandTotal).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(o => o.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(o => o.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(o => o.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(o => o.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(o => o.LastModifiedDate).IsRequired(false);
            entityTypeBuilder.HasOne(o => o.ShippingAddress).WithMany().OnDelete(DeleteBehavior.Restrict);
            entityTypeBuilder.HasOne(o => o.BillingAddress).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
        public static void ConfigureOrderItem(EntityTypeBuilder<OrderItem> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("OrderItem").HasKey(oi => oi.OrderItemId);
            entityTypeBuilder.Property(oi => oi.OrderItemId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(oi => oi.Quantity).IsRequired();
            entityTypeBuilder.Property(oi => oi.UnitPrice).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(oi => oi.TotalPrice).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(oi => oi.ProductId).IsRequired();
            entityTypeBuilder.Property(oi => oi.OrderId).IsRequired();
            entityTypeBuilder.Property(oi => oi.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(oi => oi.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(oi => oi.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(oi => oi.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(oi => oi.LastModifiedDate).IsRequired(false);
        }
        public static void ConfigureOrderPaymentAssociation(EntityTypeBuilder<OrderPaymentAssociation> entityTypeBuilder)
        {
            //entityTypeBuilder.ToTable("OrderPaymentAssociation").HasKey(opa => opa.OrderPaymentAssociationId);
            entityTypeBuilder.ToTable("OrderPaymentAssociation").Property(opa => opa.OrderPaymentAssociationId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(opa => opa.OrderId);
            entityTypeBuilder.Property(opa => opa.PaymentId);
            entityTypeBuilder.Property(opa => opa.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(opa => opa.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(opa => opa.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(opa => opa.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(opa => opa.LastModifiedDate).IsRequired(false);
            entityTypeBuilder.HasKey(psa => new { psa.OrderId, psa.PaymentId });
            entityTypeBuilder.HasOne(opa => opa.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(opa => opa.OrderId);
        }
        public static void ConfigurePayment(EntityTypeBuilder<Payment> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Payment").HasKey(o => o.PaymentId);
            entityTypeBuilder.Property(p => p.PaymentId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(p => p.GrandTotal).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(p => p.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(p => p.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(p => p.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(p => p.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(p => p.LastModifiedDate).IsRequired(false);
        }
        public static void ConfigurePaymentItem(EntityTypeBuilder<PaymentItem> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("PaymentItem").HasKey(pi => pi.PaymentItemId);
            entityTypeBuilder.Property(pi => pi.PaymentItemId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(pi => pi.Amount).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(pi => pi.Method);
            entityTypeBuilder.Property(pi => pi.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(pi => pi.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(pi => pi.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(pi => pi.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(pi => pi.LastModifiedDate).IsRequired(false);
        }
        public static void ConfigureProduct(EntityTypeBuilder<Product> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Product").HasKey(p => p.ProductId);
            entityTypeBuilder.Property(p => p.ProductId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(p => p.Code).IsRequired(true).HasMaxLength(100);
            entityTypeBuilder.Property(p => p.Name).IsRequired(true).HasMaxLength(100);
            entityTypeBuilder.Property(p => p.Summary);
            entityTypeBuilder.Property(p => p.Description);
            entityTypeBuilder.Property(p => p.ImageFile).HasMaxLength(200);
            entityTypeBuilder.Property(p => p.UnitPrice).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(p => p.UnitsInStock);
            entityTypeBuilder.Property(p => p.Star);
            entityTypeBuilder.Property(p => p.CategoryId);
            entityTypeBuilder.Property(p => p.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(p => p.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(p => p.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(p => p.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(p => p.LastModifiedDate).IsRequired(false);
        }
        public static void ConfigureProductSpecificationAssociation(EntityTypeBuilder<ProductSpecificationAssociation> entityTypeBuilder)
        {
            //entityTypeBuilder.ToTable("ProductSpecificationAssociation").HasKey(psa => new { psa.ProductId, psa.SpecificationId });
            //entityTypeBuilder.HasOne(psa => psa.Product).WithMany(p => p.Specifications).HasForeignKey(psa => psa.ProductId);
            entityTypeBuilder.ToTable("ProductSpecificationAssociation").Property(psa => psa.ProductSpecificationAssociationId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(psa => psa.ProductId);
            entityTypeBuilder.Property(psa => psa.SpecificationId);
            entityTypeBuilder.Property(psa => psa.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(psa => psa.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(psa => psa.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(psa => psa.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(psa => psa.LastModifiedDate).IsRequired(false);
            entityTypeBuilder.HasKey(psa => new { psa.ProductId, psa.SpecificationId });
            entityTypeBuilder.HasOne(psa => psa.Product).WithMany(p => p.Specifications).HasForeignKey(psa => psa.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
        public static void ConfigureSpecification(EntityTypeBuilder<Specification> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Specification").HasKey(s => s.SpecificationId);
            entityTypeBuilder.Property(s => s.SpecificationId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(s => s.Name).HasMaxLength(100);
            entityTypeBuilder.Property(s => s.Description);
            entityTypeBuilder.Property(s => s.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(s => s.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(s => s.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(s => s.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(s => s.LastModifiedDate).IsRequired(false);
        }
        public static void ConfigureUser(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("User").HasKey(u => u.UserId);
            entityTypeBuilder.Property(u => u.UserId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(u => u.UserName).IsRequired();
            entityTypeBuilder.Property<byte[]>("PasswordHash").HasColumnType("varbinary(max)");
            entityTypeBuilder.Property<byte[]>("PasswordSalt").HasColumnType("varbinary(max)");
            entityTypeBuilder.Property(d => d.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(d => d.CreatedBy).IsRequired(true);
            entityTypeBuilder.Property(d => d.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(d => d.LastModifiedBy).IsRequired(false);
            entityTypeBuilder.Property(d => d.LastModifiedDate).IsRequired(false);
            //entityTypeBuilder.ToTable("User").HasKey(u => u.UserName);
        }
        //public static void Build(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<ProductSpecificationAssociation>()
        //    //    .HasKey(psa => new { psa.ProductId, psa.SpecificationId });

        //    //modelBuilder.Entity<ProductSpecificationAssociation>()
        //    //    .HasOne(psa => psa.Product)
        //    //    .WithMany(p => p.Specifications)
        //    //    .HasForeignKey(psa => psa.ProductId);

        //    //modelBuilder.Entity<OrderPaymentAssociation>()
        //    //    .HasKey(psa => new { psa.OrderId, psa.PaymentId });

        //    //modelBuilder.Entity<OrderPaymentAssociation>()
        //    //    .HasOne(opa => opa.Order)
        //    //    .WithMany(o => o.Payments)
        //    //    .HasForeignKey(opa => opa.OrderId);

        //    //modelBuilder.Entity<ContractPaymentAssociation>()
        //    //    .HasKey(psa => new { psa.ContractId, psa.PaymentId });

        //    //modelBuilder.Entity<ContractPaymentAssociation>()
        //    //    .HasOne(cpa => cpa.Contract)
        //    //    .WithMany(c => c.Payments)
        //    //    .HasForeignKey(cpa => cpa.ContractId);
        //}
    }
}
