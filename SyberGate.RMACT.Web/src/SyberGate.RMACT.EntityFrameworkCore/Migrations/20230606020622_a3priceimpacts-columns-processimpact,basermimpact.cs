using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class a3priceimpactscolumnsprocessimpactbasermimpact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ProcessImpact",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RMImpactt",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RawMaterialGroup",
                table: "A3PriceImpacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessImpact",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RMImpactt",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RawMaterialGroup",
                table: "A3PriceImpacts");
        }
    }
}
