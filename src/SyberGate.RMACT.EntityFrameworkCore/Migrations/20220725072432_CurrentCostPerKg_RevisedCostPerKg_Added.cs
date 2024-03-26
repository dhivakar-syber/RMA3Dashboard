using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class CurrentCostPerKg_RevisedCostPerKg_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentCostPer",
                table: "A3SubPartImpacts",
                type: "decimal(18,5)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RevisedCostPer",
                table: "A3SubPartImpacts",
                type: "decimal(18,5)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentCostPer",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RevisedCostPer",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentCostPer",
                table: "A3SubPartImpacts");

            migrationBuilder.DropColumn(
                name: "RevisedCostPer",
                table: "A3SubPartImpacts");

            migrationBuilder.DropColumn(
                name: "CurrentCostPer",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RevisedCostPer",
                table: "A3PriceImpacts");
        }
    }
}
