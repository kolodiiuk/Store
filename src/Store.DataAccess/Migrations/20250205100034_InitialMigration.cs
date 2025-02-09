using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Store.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "coupon",
                columns: table => new
                {
                    coupon_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    percentage = table.Column<double>(type: "double", nullable: false),
                    start_date = table.Column<DateTime>(type: "DATE", nullable: false),
                    end_date = table.Column<DateTime>(type: "DATE", nullable: false),
                    used_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coupon", x => x.coupon_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    category = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    price_per_unit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    unit_type = table.Column<int>(type: "int", nullable: false),
                    is_available = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.product_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Salt = table.Column<string>(type: "longtext", nullable: true),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_coupon",
                columns: table => new
                {
                    product_coupon_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    coupon_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_coupon", x => x.product_coupon_id);
                    table.ForeignKey(
                        name: "FK_product_coupon_coupon_coupon_id",
                        column: x => x.coupon_id,
                        principalTable: "coupon",
                        principalColumn: "coupon_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_coupon_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    address_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    apartments = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    house = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    street = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    district = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.address_id);
                    table.ForeignKey(
                        name: "FK_address_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "basket_item",
                columns: table => new
                {
                    basket_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basket_item", x => x.basket_item_id);
                    table.ForeignKey(
                        name: "FK_basket_item_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_basket_item_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    status = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    payment_method = table.Column<int>(type: "int", nullable: false),
                    payment_status = table.Column<int>(type: "int", nullable: false),
                    has_coupon = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    delivery_fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    payment_intent_id = table.Column<string>(type: "CHAR(27)", nullable: true),
                    coupon_id = table.Column<int>(type: "int", nullable: true),
                    collected_date = table.Column<DateTime>(type: "DATE", nullable: true),
                    delivered_date = table.Column<DateTime>(type: "DATE", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    address_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_orders_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_orders_coupon_coupon_id",
                        column: x => x.coupon_id,
                        principalTable: "coupon",
                        principalColumn: "coupon_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_orders_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "feedback",
                columns: table => new
                {
                    feedback_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    rating = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedback", x => x.feedback_id);
                    table.CheckConstraint("CK_Feedback_Rating", "Rating >= 1 AND Rating <= 5");
                    table.ForeignKey(
                        name: "FK_feedback_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    order_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    current_price_per_unit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    order_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_item", x => x.order_item_id);
                    table.ForeignKey(
                        name: "FK_order_item_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_item_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_address_user_id",
                table: "address",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_basket_item_product_id",
                table: "basket_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_basket_item_user_id",
                table: "basket_item",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_order_id",
                table: "feedback",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_order_id",
                table: "order_item",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_product_id",
                table: "order_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_address_id",
                table: "orders",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_coupon_id",
                table: "orders",
                column: "coupon_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_coupon_coupon_id",
                table: "product_coupon",
                column: "coupon_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_coupon_product_id",
                table: "product_coupon",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basket_item");

            migrationBuilder.DropTable(
                name: "feedback");

            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "product_coupon");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "coupon");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
