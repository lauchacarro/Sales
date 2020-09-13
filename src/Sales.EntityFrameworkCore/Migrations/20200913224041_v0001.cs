using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sales.Migrations
{
    public partial class v0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    OrderId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Attempts = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Duration = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plans_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSales",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvocePaymentProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InvoceId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    PaymentProviderId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Transaction = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Link = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvocePaymentProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvocePaymentProviders_Invoices_InvoceId",
                        column: x => x.InvoceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvocePaymentProviders_PaymentProviders_PaymentProviderId",
                        column: x => x.PaymentProviderId,
                        principalTable: "PaymentProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlanId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Currency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanPrices_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlanId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSaleOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductSaleId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    OrderId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSaleOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSaleOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSaleOrders_ProductSales_ProductSaleId",
                        column: x => x.ProductSaleId,
                        principalTable: "ProductSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSalePrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductSaleId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Currency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSalePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSalePrices_ProductSales_ProductSaleId",
                        column: x => x.ProductSaleId,
                        principalTable: "ProductSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoceWebhooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InvocePaymentProviderId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoceWebhooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoceWebhooks_InvocePaymentProviders_InvocePaymentProviderId",
                        column: x => x.InvocePaymentProviderId,
                        principalTable: "InvocePaymentProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionCycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubscriptionId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ActivationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionCycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionCycles_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionCycleOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubscriptionCycleId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    OrderId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionCycleOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionCycleOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionCycleOrders_SubscriptionCycles_SubscriptionCycleId",
                        column: x => x.SubscriptionCycleId,
                        principalTable: "SubscriptionCycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvocePaymentProviders_InvoceId",
                table: "InvocePaymentProviders",
                column: "InvoceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvocePaymentProviders_Link",
                table: "InvocePaymentProviders",
                column: "Link",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvocePaymentProviders_PaymentProviderId",
                table: "InvocePaymentProviders",
                column: "PaymentProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_InvocePaymentProviders_Transaction",
                table: "InvocePaymentProviders",
                column: "Transaction",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoceWebhooks_InvocePaymentProviderId",
                table: "InvoceWebhooks",
                column: "InvocePaymentProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                table: "Invoices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_OrderId",
                table: "Notifications",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviders_Name",
                table: "PaymentProviders",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanPrices_PlanId",
                table: "PlanPrices",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ProductId",
                table: "Plans",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSaleOrders_OrderId",
                table: "ProductSaleOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSaleOrders_ProductSaleId",
                table: "ProductSaleOrders",
                column: "ProductSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSalePrices_ProductSaleId",
                table: "ProductSalePrices",
                column: "ProductSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProductId",
                table: "ProductSales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionCycleOrders_OrderId",
                table: "SubscriptionCycleOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionCycleOrders_SubscriptionCycleId",
                table: "SubscriptionCycleOrders",
                column: "SubscriptionCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionCycles_SubscriptionId",
                table: "SubscriptionCycles",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PlanId",
                table: "Subscriptions",
                column: "PlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoceWebhooks");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PlanPrices");

            migrationBuilder.DropTable(
                name: "ProductSaleOrders");

            migrationBuilder.DropTable(
                name: "ProductSalePrices");

            migrationBuilder.DropTable(
                name: "SubscriptionCycleOrders");

            migrationBuilder.DropTable(
                name: "InvocePaymentProviders");

            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropTable(
                name: "SubscriptionCycles");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PaymentProviders");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
