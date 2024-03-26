using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class BaseRMRateTable_newfieldsfor_Index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromPeriod",
                table: "BaseRMRates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndexName",
                table: "BaseRMRates",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IndexValue",
                table: "BaseRMRates",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ToPeriod",
                table: "BaseRMRates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromPeriod",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "IndexName",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "IndexValue",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "ToPeriod",
                table: "BaseRMRates");
        }
    }
}
