using Microsoft.EntityFrameworkCore.Migrations;

namespace PPETracker.Data.Migrations
{
    public partial class CorrectName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GloveThickeness",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "GloveThickness",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GloveThickness",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "GloveThickeness",
                table: "Products",
                type: "int",
                nullable: true);
        }
    }
}
