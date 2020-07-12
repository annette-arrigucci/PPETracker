using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PPETracker.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    CategoryID = table.Column<int>(nullable: false),
                    PhotoLink = table.Column<string>(maxLength: 50, nullable: true),
                    Brand = table.Column<string>(maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Comments = table.Column<string>(maxLength: 100, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    CanisterType = table.Column<string>(maxLength: 50, nullable: true),
                    GasMaskAssociatedWith = table.Column<string>(maxLength: 50, nullable: true),
                    GasMaskType = table.Column<string>(maxLength: 50, nullable: true),
                    GloveQuantity = table.Column<int>(nullable: true),
                    GloveSize = table.Column<string>(maxLength: 10, nullable: true),
                    GloveThickeness = table.Column<int>(nullable: true),
                    GogglesType = table.Column<string>(maxLength: 50, nullable: true),
                    NumOunces = table.Column<int>(nullable: true),
                    SanitizerType = table.Column<string>(maxLength: 30, nullable: true),
                    MaskType = table.Column<string>(maxLength: 50, nullable: true),
                    WipeQuantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledShipDate = table.Column<DateTime>(nullable: false),
                    ActualShipDate = table.Column<DateTime>(nullable: true),
                    RecipientID = table.Column<int>(nullable: false),
                    ShipStatus = table.Column<string>(maxLength: 50, nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Shipments_Recipients_RecipientID",
                        column: x => x.RecipientID,
                        principalTable: "Recipients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentProducts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShipmentProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentProducts_Shipments_ShipmentID",
                        column: x => x.ShipmentID,
                        principalTable: "Shipments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentProducts_ProductID",
                table: "ShipmentProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentProducts_ShipmentID",
                table: "ShipmentProducts",
                column: "ShipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_RecipientID",
                table: "Shipments",
                column: "RecipientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipmentProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Recipients");
        }
    }
}
