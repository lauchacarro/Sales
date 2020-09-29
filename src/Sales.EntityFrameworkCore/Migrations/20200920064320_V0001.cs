using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Sales.EntityFrameworkCore.Migrations
{
    public partial class V0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sale");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "sale",
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
                name: "Products",
                schema: "sale",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "sale",
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
                        principalSchema: "sale",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "sale",
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
                        principalSchema: "sale",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                schema: "sale",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
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
                        principalSchema: "sale",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSales",
                schema: "sale",
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
                        principalSchema: "sale",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePaymentProviders",
                schema: "sale",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InvoceId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    Transaction = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Link = table.Column<string>(unicode: false, maxLength: 5000, nullable: false),
                    Provider = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePaymentProviders_Invoices_InvoceId",
                        column: x => x.InvoceId,
                        principalSchema: "sale",
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanPrices",
                schema: "sale",
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
                        principalSchema: "sale",
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "sale",
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
                        principalSchema: "sale",
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSaleOrders",
                schema: "sale",
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
                        principalSchema: "sale",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSaleOrders_ProductSales_ProductSaleId",
                        column: x => x.ProductSaleId,
                        principalSchema: "sale",
                        principalTable: "ProductSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSalePrices",
                schema: "sale",
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
                        principalSchema: "sale",
                        principalTable: "ProductSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceWebhooks",
                schema: "sale",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InvocePaymentProviderId = table.Column<Guid>(unicode: false, maxLength: 50, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceWebhooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceWebhooks_InvoicePaymentProviders_InvocePaymentProviderId",
                        column: x => x.InvocePaymentProviderId,
                        principalSchema: "sale",
                        principalTable: "InvoicePaymentProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionCycles",
                schema: "sale",
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
                        principalSchema: "sale",
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionCycleOrders",
                schema: "sale",
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
                        principalSchema: "sale",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionCycleOrders_SubscriptionCycles_SubscriptionCycleId",
                        column: x => x.SubscriptionCycleId,
                        principalSchema: "sale",
                        principalTable: "SubscriptionCycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentProviders_InvoceId",
                schema: "sale",
                table: "InvoicePaymentProviders",
                column: "InvoceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentProviders_Link",
                schema: "sale",
                table: "InvoicePaymentProviders",
                column: "Link",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentProviders_Transaction",
                schema: "sale",
                table: "InvoicePaymentProviders",
                column: "Transaction",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                schema: "sale",
                table: "Invoices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceWebhooks_InvocePaymentProviderId",
                schema: "sale",
                table: "InvoiceWebhooks",
                column: "InvocePaymentProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_OrderId",
                schema: "sale",
                table: "Notifications",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanPrices_PlanId",
                schema: "sale",
                table: "PlanPrices",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ProductId",
                schema: "sale",
                table: "Plans",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                schema: "sale",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSaleOrders_OrderId",
                schema: "sale",
                table: "ProductSaleOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSaleOrders_ProductSaleId",
                schema: "sale",
                table: "ProductSaleOrders",
                column: "ProductSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSalePrices_ProductSaleId",
                schema: "sale",
                table: "ProductSalePrices",
                column: "ProductSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProductId",
                schema: "sale",
                table: "ProductSales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionCycleOrders_OrderId",
                schema: "sale",
                table: "SubscriptionCycleOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionCycleOrders_SubscriptionCycleId",
                schema: "sale",
                table: "SubscriptionCycleOrders",
                column: "SubscriptionCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionCycles_SubscriptionId",
                schema: "sale",
                table: "SubscriptionCycles",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PlanId",
                schema: "sale",
                table: "Subscriptions",
                column: "PlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceWebhooks",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "PlanPrices",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "ProductSaleOrders",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "ProductSalePrices",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "SubscriptionCycleOrders",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "InvoicePaymentProviders",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "ProductSales",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "SubscriptionCycles",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "Plans",
                schema: "sale");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "sale");
        }
    }
}
