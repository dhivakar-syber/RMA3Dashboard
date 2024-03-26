using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class ConversionCost_Added_In_A3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ConversionCost",
                table: "A3SubPartImpacts",
                type: "decimal(18,5)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ConversionCost",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConversionCost",
                table: "A3SubPartImpacts");

            migrationBuilder.DropColumn(
                name: "ConversionCost",
                table: "A3PriceImpacts");
        }
    }
}
