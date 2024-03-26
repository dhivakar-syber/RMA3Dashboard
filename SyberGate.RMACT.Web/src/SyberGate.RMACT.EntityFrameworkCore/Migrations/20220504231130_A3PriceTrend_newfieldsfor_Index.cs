using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class A3PriceTrend_newfieldsfor_Index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RevFromPeriod",
                table: "A3PriceTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevIndexName",
                table: "A3PriceTrends",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RevIndexValue",
                table: "A3PriceTrends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevToPeriod",
                table: "A3PriceTrends",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RevFromPeriod",
                table: "A3PriceTrends");

            migrationBuilder.DropColumn(
                name: "RevIndexName",
                table: "A3PriceTrends");

            migrationBuilder.DropColumn(
                name: "RevIndexValue",
                table: "A3PriceTrends");

            migrationBuilder.DropColumn(
                name: "RevToPeriod",
                table: "A3PriceTrends");
        }
    }
}
