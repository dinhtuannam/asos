using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.API.Migrations
{
    /// <inheritdoc />
    public partial class dborderinginit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_order_status",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_order_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_refunds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefundAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_refunds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SubPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PointUsed = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    StatusId = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_orders_tb_order_status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "tb_order_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_order_histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_order_histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_order_histories_tb_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tb_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_order_items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Stock = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_order_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_order_items_tb_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tb_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefundId = table.Column<Guid>(type: "uuid", nullable: true),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    BankBranch = table.Column<string>(type: "text", nullable: false),
                    BankNumber = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_transactions_tb_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tb_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_transactions_tb_refunds_RefundId",
                        column: x => x.RefundId,
                        principalTable: "tb_refunds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_order_histories_OrderId",
                table: "tb_order_histories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_order_items_OrderId",
                table: "tb_order_items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_order_items_ProductId",
                table: "tb_order_items",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_orders_DiscountId",
                table: "tb_orders",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_orders_StatusId",
                table: "tb_orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_orders_UserId",
                table: "tb_orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_transactions_OrderId",
                table: "tb_transactions",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_transactions_RefundId",
                table: "tb_transactions",
                column: "RefundId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_order_histories");

            migrationBuilder.DropTable(
                name: "tb_order_items");

            migrationBuilder.DropTable(
                name: "tb_transactions");

            migrationBuilder.DropTable(
                name: "tb_orders");

            migrationBuilder.DropTable(
                name: "tb_refunds");

            migrationBuilder.DropTable(
                name: "tb_order_status");
        }
    }
}
