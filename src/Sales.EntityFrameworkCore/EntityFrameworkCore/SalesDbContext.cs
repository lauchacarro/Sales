
using System;

using Abp.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Products;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Options;

namespace Sales.EntityFrameworkCore
{
    public partial class SalesDbContext : AbpDbContext
    {
        private readonly IDatabaseOptions _databaseOptions;
        public SalesDbContext(IDatabaseOptions databaseOptions, DbContextOptions<SalesDbContext> options)
            : base(options)
        {
            _databaseOptions = databaseOptions;
        }

        public virtual DbSet<InvoicePaymentProvider> InvoicePaymentProviders { get; set; }
        public virtual DbSet<InvoiceWebhook> InvoiceWebhooks { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PlanPrice> PlanPrices { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<ProductSaleOrder> ProductSaleOrders { get; set; }
        public virtual DbSet<ProductSalePrice> ProductSalePrices { get; set; }
        public virtual DbSet<ProductSale> ProductSales { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SubscriptionCycleOrder> SubscriptionCycleOrders { get; set; }
        public virtual DbSet<SubscriptionCycle> SubscriptionCycles { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_databaseOptions.Schema);


            modelBuilder.Entity<InvoicePaymentProvider>(entity =>
            {
                entity.HasIndex(e => e.Link)
                    .IsUnique();

                entity.HasIndex(e => e.Transaction)
                    .IsUnique();

                entity.HasKey(p => p.Id);

                entity.Property(e => e.InvoceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .HasConversion(v => v.ToString(), v => new Uri(v))
                    .IsUnicode(false);

                entity.Property(e => e.Transaction)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Invoce)
                    .WithMany(p => p.InvocePaymentProviders)
                    .HasForeignKey(d => d.InvoceId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.OwnsOne(d => d.PaymentProvider, st =>
                 {
                     st.Property(e => e.Provider).HasConversion<string>().IsRequired().HasColumnName(nameof(PaymentProvider.Provider));
                 });
            });


            modelBuilder.Entity<InvoiceWebhook>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.InvocePaymentProviderId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.InvocePaymentProvider)
                    .WithMany(p => p.InvoceWebhooks)
                    .HasForeignKey(d => d.InvocePaymentProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.OwnsOne(d => d.Status, st =>
                {
                    st.Property(e => e.Status).HasConversion<string>().IsRequired().HasColumnName(nameof(InvoiceWebhook.Status));
                });
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.OwnsOne(s => s.Status, st =>
                {
                    st.Property(e => e.Status).HasConversion<string>().IsRequired().HasColumnName(nameof(Invoice.Status));
                });

                entity.OwnsOne(s => s.Type, t =>
                {
                    t.Property(e => e.Type).HasConversion<string>().IsRequired().HasColumnName(nameof(Invoice.Type));
                });

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });


            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.OwnsOne(s => s.Type, t =>
                {
                    t.Property(e => e.Type).HasConversion<string>().IsRequired().HasColumnName(nameof(Notification.Type));
                });

                entity.OwnsOne(s => s.Status, st =>
                {
                    st.Property(e => e.Status).HasConversion<string>().IsRequired().HasColumnName(nameof(Notification.Status));
                });

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.OwnsOne(s => s.Currency, t =>
                {
                    t.Property(e => e.Code).HasConversion<string>().IsRequired().HasColumnName(nameof(Order.Currency));
                });

                entity.OwnsOne(s => s.Status, st =>
                {
                    st.Property(e => e.Status).HasConversion<string>().IsRequired().HasColumnName(nameof(Order.Status));
                });

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.LastModificationTime).HasColumnType("datetime");

                entity.OwnsOne(s => s.Type, t =>
                {
                    t.Property(e => e.Type).HasConversion<string>().IsRequired().HasColumnName(nameof(Order.Type));
                });

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlanPrice>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.OwnsOne(s => s.Currency, t =>
                {
                    t.Property(e => e.Code).HasConversion<string>().IsRequired().HasColumnName(nameof(Order.Currency));
                });

                entity.Property(e => e.PlanId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlanPrices)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.OwnsOne(s => s.Status, st =>
                {
                    st.Property(e => e.Status).HasConversion<string>().IsRequired().HasColumnName(nameof(Plan.Status));
                });

                entity.OwnsOne(s => s.Type, t =>
                {
                    t.Property(e => e.Type).HasConversion<string>().IsRequired().HasColumnName(nameof(Plan.Type));
                });

                entity.OwnsOne(s => s.Duration, t =>
                {
                    t.Property(e => e.Duration).HasConversion<string>().IsRequired().HasColumnName(nameof(Plan.Duration));
                });

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProductSaleOrder>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductSaleId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductSaleOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ProductSale)
                    .WithMany(p => p.ProductSaleOrders)
                    .HasForeignKey(d => d.ProductSaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProductSalePrice>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.OwnsOne(s => s.Currency, t =>
                {
                    t.Property(e => e.Code).HasConversion<string>().IsRequired().HasColumnName(nameof(Order.Currency));
                });

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProductSaleId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProductSale)
                    .WithMany(p => p.ProductSalePrices)
                    .HasForeignKey(d => d.ProductSaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProductSale>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.HasKey(p => p.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.OwnsOne(s => s.Status, st =>
                {
                    st.Property(e => e.Status).HasConversion<string>().IsRequired().HasColumnName(nameof(Product.Status));
                });

                entity.OwnsOne(s => s.Type, t =>
                {
                    t.Property(e => e.Type).HasConversion<string>().IsRequired().HasColumnName(nameof(Product.Type));
                });

            });

            modelBuilder.Entity<SubscriptionCycleOrder>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubscriptionCycleId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.SubscriptionCycleOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.SubscriptionCycle)
                    .WithMany(p => p.SubscriptionCycleOrders)
                    .HasForeignKey(d => d.SubscriptionCycleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });


            modelBuilder.Entity<SubscriptionCycle>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.ActivationDate).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.OwnsOne(s => s.Status, st =>
                {
                    st.Property(e => e.Status).HasConversion<string>().IsRequired().HasColumnName(nameof(SubscriptionCycle.Status));
                });

                entity.Property(e => e.SubscriptionId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.SubscriptionCycles)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.PlanId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.OwnsOne(s => s.Status, st =>
                {
                    st.Property(e => e.Status).HasConversion<string>().IsRequired().HasColumnName(nameof(Subscription.Status));
                });

                entity.OwnsOne(s => s.Type, st =>
                {
                    st.Property(e => e.Type).HasConversion<string>().IsRequired().HasColumnName(nameof(Subscription.Type));
                });

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });
        }
    }
}
