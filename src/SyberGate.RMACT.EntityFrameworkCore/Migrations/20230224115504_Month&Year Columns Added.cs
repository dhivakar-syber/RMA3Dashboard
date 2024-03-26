using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class MonthYearColumnsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "PartBuckets",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "PartBuckets",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "A3PartBuckets",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "A3PartBuckets",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "PartBuckets");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "PartBuckets");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "A3PartBuckets");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "A3PartBuckets");
        }
    }
}
