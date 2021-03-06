﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sales.EntityFrameworkCore;

namespace Sales.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(SalesDbContext))]
    partial class SalesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("sale")
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sales.Domain.Entities.Invoices.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Invoices.InvoicePaymentProvider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InvoceId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("varchar(5000)")
                        .HasMaxLength(5000)
                        .IsUnicode(false);

                    b.Property<string>("Transaction")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("InvoceId");

                    b.HasIndex("Link")
                        .IsUnique();

                    b.HasIndex("Transaction")
                        .IsUnique();

                    b.ToTable("InvoicePaymentProviders");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Invoices.InvoiceWebhook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid>("InvocePaymentProviderId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("InvocePaymentProviderId");

                    b.ToTable("InvoiceWebhooks");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Notifications.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Attempts")
                        .HasColumnType("int");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Plans.Plan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Plans.PlanPrice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("PlanPrices");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Products.ProductSale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductSales");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Products.ProductSaleOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<Guid>("ProductSaleId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductSaleId");

                    b.ToTable("ProductSaleOrders");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Products.ProductSalePrice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<Guid>("ProductSaleId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("ProductSaleId");

                    b.ToTable("ProductSalePrices");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Subscriptions.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Subscriptions.SubscriptionCycle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ActivationDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("SubscriptionCycles");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Subscriptions.SubscriptionCycleOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<Guid>("SubscriptionCycleId")
                        .HasColumnType("uniqueidentifier")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("SubscriptionCycleId");

                    b.ToTable("SubscriptionCycleOrders");
                });

            modelBuilder.Entity("Sales.Domain.Entities.Invoices.Invoice", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Orders.Order", "Order")
                        .WithMany("Invoices")
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.OwnsOne("Sales.Domain.ValueObjects.Invoices.InvoiceStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("InvoiceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasColumnName("Status")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("InvoiceId");

                            b1.ToTable("Invoices");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceId");
                        });

                    b.OwnsOne("Sales.Domain.ValueObjects.Invoices.InvoiceType", "Type", b1 =>
                        {
                            b1.Property<Guid>("InvoiceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnName("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("InvoiceId");

                            b1.ToTable("Invoices");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Invoices.InvoicePaymentProvider", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Invoices.Invoice", "Invoce")
                        .WithMany("InvocePaymentProviders")
                        .HasForeignKey("InvoceId")
                        .IsRequired();

                    b.OwnsOne("Sales.Domain.Entities.Invoices.PaymentProvider", "PaymentProvider", b1 =>
                        {
                            b1.Property<Guid>("InvoicePaymentProviderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Provider")
                                .IsRequired()
                                .HasColumnName("Provider")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("InvoicePaymentProviderId");

                            b1.ToTable("InvoicePaymentProviders");

                            b1.WithOwner()
                                .HasForeignKey("InvoicePaymentProviderId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Invoices.InvoiceWebhook", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Invoices.InvoicePaymentProvider", "InvocePaymentProvider")
                        .WithMany("InvoceWebhooks")
                        .HasForeignKey("InvocePaymentProviderId")
                        .IsRequired();

                    b.OwnsOne("Sales.Domain.ValueObjects.Invoices.InvoiceWebhookStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("InvoiceWebhookId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasColumnName("Status")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("InvoiceWebhookId");

                            b1.ToTable("InvoiceWebhooks");

                            b1.WithOwner()
                                .HasForeignKey("InvoiceWebhookId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Notifications.Notification", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Orders.Order", "Order")
                        .WithMany("Notifications")
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.OwnsOne("Sales.Domain.ValueObjects.Notifications.NotificationStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("NotificationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasColumnName("Status")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("NotificationId");

                            b1.ToTable("Notifications");

                            b1.WithOwner()
                                .HasForeignKey("NotificationId");
                        });

                    b.OwnsOne("Sales.Domain.ValueObjects.Notifications.NotificationType", "Type", b1 =>
                        {
                            b1.Property<Guid>("NotificationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnName("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("NotificationId");

                            b1.ToTable("Notifications");

                            b1.WithOwner()
                                .HasForeignKey("NotificationId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Orders.Order", b =>
                {
                    b.OwnsOne("Sales.Domain.ValueObjects.Currency", "Currency", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnName("Currency")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("Sales.Domain.ValueObjects.Orders.OrderStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasColumnName("Status")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("Sales.Domain.ValueObjects.Orders.OrderType", "Type", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnName("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Plans.Plan", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Products.Product", "Product")
                        .WithMany("Plans")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.OwnsOne("Sales.Domain.ValueObjects.Plans.PlanCycleDuration", "Duration", b1 =>
                        {
                            b1.Property<Guid>("PlanId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Duration")
                                .IsRequired()
                                .HasColumnName("Duration")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PlanId");

                            b1.ToTable("Plans");

                            b1.WithOwner()
                                .HasForeignKey("PlanId");
                        });

                    b.OwnsOne("Sales.Domain.ValueObjects.Plans.PlanStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("PlanId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasColumnName("Status")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PlanId");

                            b1.ToTable("Plans");

                            b1.WithOwner()
                                .HasForeignKey("PlanId");
                        });

                    b.OwnsOne("Sales.Domain.ValueObjects.Plans.PlanType", "Type", b1 =>
                        {
                            b1.Property<Guid>("PlanId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnName("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PlanId");

                            b1.ToTable("Plans");

                            b1.WithOwner()
                                .HasForeignKey("PlanId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Plans.PlanPrice", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Plans.Plan", "Plan")
                        .WithMany("PlanPrices")
                        .HasForeignKey("PlanId")
                        .IsRequired();

                    b.OwnsOne("Sales.Domain.ValueObjects.Currency", "Currency", b1 =>
                        {
                            b1.Property<Guid>("PlanPriceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnName("Currency")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PlanPriceId");

                            b1.ToTable("PlanPrices");

                            b1.WithOwner()
                                .HasForeignKey("PlanPriceId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Products.Product", b =>
                {
                    b.OwnsOne("Sales.Domain.ValueObjects.Products.ProductStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasColumnName("Status")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Sales.Domain.ValueObjects.Products.ProductType", "Type", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnName("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Products.ProductSale", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Products.Product", "Product")
                        .WithMany("ProductSales")
                        .HasForeignKey("ProductId")
                        .IsRequired();
                });

            modelBuilder.Entity("Sales.Domain.Entities.Products.ProductSaleOrder", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Orders.Order", "Order")
                        .WithMany("ProductSaleOrders")
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.HasOne("Sales.Domain.Entities.Products.ProductSale", "ProductSale")
                        .WithMany("ProductSaleOrders")
                        .HasForeignKey("ProductSaleId")
                        .IsRequired();
                });

            modelBuilder.Entity("Sales.Domain.Entities.Products.ProductSalePrice", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Products.ProductSale", "ProductSale")
                        .WithMany("ProductSalePrices")
                        .HasForeignKey("ProductSaleId")
                        .IsRequired();

                    b.OwnsOne("Sales.Domain.ValueObjects.Currency", "Currency", b1 =>
                        {
                            b1.Property<Guid>("ProductSalePriceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnName("Currency")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductSalePriceId");

                            b1.ToTable("ProductSalePrices");

                            b1.WithOwner()
                                .HasForeignKey("ProductSalePriceId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Subscriptions.Subscription", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Plans.Plan", "Plan")
                        .WithMany("Subscriptions")
                        .HasForeignKey("PlanId")
                        .IsRequired();

                    b.OwnsOne("Sales.Domain.ValueObjects.Subscriptions.SubscriptionStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("SubscriptionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasColumnName("Status")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SubscriptionId");

                            b1.ToTable("Subscriptions");

                            b1.WithOwner()
                                .HasForeignKey("SubscriptionId");
                        });

                    b.OwnsOne("Sales.Domain.ValueObjects.Subscriptions.SubscriptionType", "Type", b1 =>
                        {
                            b1.Property<Guid>("SubscriptionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnName("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SubscriptionId");

                            b1.ToTable("Subscriptions");

                            b1.WithOwner()
                                .HasForeignKey("SubscriptionId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Subscriptions.SubscriptionCycle", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Subscriptions.Subscription", "Subscription")
                        .WithMany("SubscriptionCycles")
                        .HasForeignKey("SubscriptionId")
                        .IsRequired();

                    b.OwnsOne("Sales.Domain.ValueObjects.Subscriptions.SubscriptionCycleStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("SubscriptionCycleId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasColumnName("Status")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SubscriptionCycleId");

                            b1.ToTable("SubscriptionCycles");

                            b1.WithOwner()
                                .HasForeignKey("SubscriptionCycleId");
                        });
                });

            modelBuilder.Entity("Sales.Domain.Entities.Subscriptions.SubscriptionCycleOrder", b =>
                {
                    b.HasOne("Sales.Domain.Entities.Orders.Order", "Order")
                        .WithMany("SubscriptionCycleOrders")
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.HasOne("Sales.Domain.Entities.Subscriptions.SubscriptionCycle", "SubscriptionCycle")
                        .WithMany("SubscriptionCycleOrders")
                        .HasForeignKey("SubscriptionCycleId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
