using Microsoft.EntityFrameworkCore.Migrations;

namespace PPETracker.Data.Migrations
{
    public partial class UpdateShipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "ShipStatus",
                table: "Shipments",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Shipments",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Shipments",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "ShipStatus",
                table: "Shipments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
