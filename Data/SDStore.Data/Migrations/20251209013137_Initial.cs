using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SDStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceMode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.CheckConstraint("CK_Item_PriceMode_AllowedCurrencies", "[PriceMode] IN ('PerItem', 'PerKg', 'PerLiter')");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total_Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total_VAT_Excluded = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total_Net = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.CheckConstraint("CK_Order_Currency_AllowedCurrencies", "[Currency] IN ('BGN', 'EUR', 'USD', 'CAD', 'GBP')");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTotal_Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VAT_Percentage = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    VAT_Excluded = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal_Net = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.ItemId, x.OrderId });
                    table.CheckConstraint("CK_OrderItem_Currency_AllowedCurrencies", "[Currency] IN ('BGN', 'EUR', 'USD', 'CAD', 'GBP')");
                    table.CheckConstraint("CK_OrderItem_VAT_Percentage_AllowedPercentages", "[VAT_Percentage] >= 0.00 AND [VAT_Percentage] <= 27.00");
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name", "Price", "PriceMode" },
                values: new object[,]
                {
                    { 1, "LRAD model Vučić", "{\"Amount\":10464.95,\"Currency\":\"EUR\"}", "PerItem" },
                    { 2, "Bayraktar TB2", "{\"Amount\":7000000.00,\"Currency\":\"USD\"}", "PerItem" },
                    { 3, "F-16 Block 70", "{\"Amount\":150000000.00,\"Currency\":\"USD\"}", "PerItem" },
                    { 4, "Uaz 469 restored", "{\"Amount\":10500.00,\"Currency\":\"BGN\"}", "PerItem" },
                    { 5, "Simson S50", "{\"Amount\":2999.00,\"Currency\":\"BGN\"}", "PerItem" },
                    { 6, "Cyanide", "{\"Amount\":1250.00,\"Currency\":\"USD\"}", "PerKg" },
                    { 7, "Novichok Agent", "{\"Amount\":23500.50,\"Currency\":\"BGN\"}", "PerKg" },
                    { 8, "Bulk 7.62x39mm Ammo (AR-M1)", "{\"Amount\":1755.00,\"Currency\":\"BGN\"}", "PerKg" },
                    { 9, "Ballistic Alloy Powder (Training Grade)", "{\"Amount\":1200.00,\"Currency\":\"CAD\"}", "PerKg" },
                    { 10, "Zimmerit (Anti‑Magnetic Armor Coating)", "{\"Amount\":250.50,\"Currency\":\"EUR\"}", "PerKg" },
                    { 11, "Mustard Gas", "{\"Amount\":211.20,\"Currency\":\"EUR\"}", "PerLiter" },
                    { 12, "JP-8 Kerosene", "{\"Amount\":14308.83,\"Currency\":\"USD\"}", "PerLiter" },
                    { 13, "HFD-R: Phosphoric esters (Fire Resistant)", "{\"Amount\":1442.10,\"Currency\":\"BGN\"}", "PerLiter" },
                    { 14, "Ricin", "{\"Amount\":419.30,\"Currency\":\"USD\"}", "PerLiter" },
                    { 15, "Sarin", "{\"Amount\":761.50,\"Currency\":\"GBP\"}", "PerLiter" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Address", "Currency", "Email", "PhoneNumber", "Total_Gross", "Total_Net", "Total_VAT_Excluded" },
                values: new object[,]
                {
                    { new Guid("24a2c27a-298c-4eea-bbd9-ffa832e9a697"), "789 Oak Street, Suite 210, Demo City, TX 73301", "EUR", "austrian-painter@thirdreich.de", "+1 (555) 010-0003", 12314930750.70m, 11596249563.48m, 718681187.22m },
                    { new Guid("32b00f9f-1b95-412a-8517-678c49419467"), "123 Maplewood Avenue, Apt 4B, Example City, CA 90001", "BGN", "todor.jivkov@prb.bg", "+1 (555) 010-0001", 4029616498.00m, 3504013748.33m, 525602749.67m },
                    { new Guid("920c12b3-52da-409d-833f-109f8ee0147a"), "16 Pinecrest Road, Sampleville, WA 98001", "GBP", "zelensky@example.ua", "+1 (555) 010-0004", 283997879.16m, 248286392.05m, 35711487.11m },
                    { new Guid("c131cb5b-acb4-479b-9dea-fedb9f52aef4"), "45 Riverside Drive, Example Town, NY 10010", "USD", "vlad.putin@ussr.ru", "+1 (555) 010-0002", 5381.97m, 4646.64m, 735.33m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "ItemId", "OrderId", "Amount", "Currency", "SubTotal_Gross", "SubTotal_Net", "VAT_Excluded", "VAT_Percentage" },
                values: new object[,]
                {
                    { 2, new Guid("920c12b3-52da-409d-833f-109f8ee0147a"), 15.00m, "GBP", 78808672.33m, 71644247.57m, 7164424.76m, 10.00m },
                    { 3, new Guid("32b00f9f-1b95-412a-8517-678c49419467"), 16m, "BGN", 4029600000.00m, 3504000000.00m, 525600000.00m, 15.00m },
                    { 4, new Guid("32b00f9f-1b95-412a-8517-678c49419467"), 1m, "BGN", 10500.00m, 8750.00m, 1750.00m, 20.00m },
                    { 5, new Guid("32b00f9f-1b95-412a-8517-678c49419467"), 2m, "BGN", 5998.00m, 4998.33m, 999.67m, 20.00m },
                    { 7, new Guid("c131cb5b-acb4-479b-9dea-fedb9f52aef4"), 0.21m, "USD", 2939.31m, 2409.27m, 530.04m, 22.00m },
                    { 8, new Guid("920c12b3-52da-409d-833f-109f8ee0147a"), 100000.00m, "GBP", 78453285.65m, 64305971.84m, 14147313.81m, 22.00m },
                    { 10, new Guid("24a2c27a-298c-4eea-bbd9-ffa832e9a697"), 10000.00m, "EUR", 25050000.00m, 23857142.86m, 1192857.14m, 5.00m },
                    { 11, new Guid("24a2c27a-298c-4eea-bbd9-ffa832e9a697"), 30000.00m, "EUR", 6336000.00m, 5966101.69m, 369898.31m, 6.20m },
                    { 12, new Guid("24a2c27a-298c-4eea-bbd9-ffa832e9a697"), 999999.99m, "EUR", 12283544750.70m, 11566426318.93m, 717118431.77m, 12.40m },
                    { 12, new Guid("920c12b3-52da-409d-833f-109f8ee0147a"), 10000.00m, "GBP", 107396180.46m, 95548203.26m, 11847977.20m, 12.40m },
                    { 13, new Guid("920c12b3-52da-409d-833f-109f8ee0147a"), 30000.00m, "GBP", 19339740.72m, 16787969.38m, 2551771.34m, 15.20m },
                    { 14, new Guid("c131cb5b-acb4-479b-9dea-fedb9f52aef4"), 0.72m, "USD", 301.90m, 276.97m, 24.93m, 9.00m },
                    { 15, new Guid("c131cb5b-acb4-479b-9dea-fedb9f52aef4"), 2.11m, "USD", 2140.76m, 1960.40m, 180.36m, 9.20m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
