using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Setteled_Revised_WL_Ratio_In_A3_Trend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeightRatio",
                table: "A3PriceTrends",
                newName: "SetteledWRatio");

            migrationBuilder.RenameColumn(
                name: "LossRatio",
                table: "A3PriceTrends",
                newName: "SetteledLRatio");

            migrationBuilder.AddColumn<decimal>(
                name: "RevisedLRatio",
                table: "A3PriceTrends",
                type: "decimal(18,5)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RevisedWRatio",
                table: "A3PriceTrends",
                type: "decimal(18,5)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RevisedLRatio",
                table: "A3PriceTrends");

            migrationBuilder.DropColumn(
                name: "RevisedWRatio",
                table: "A3PriceTrends");

            migrationBuilder.RenameColumn(
                name: "SetteledWRatio",
                table: "A3PriceTrends",
                newName: "WeightRatio");

            migrationBuilder.RenameColumn(
                name: "SetteledLRatio",
                table: "A3PriceTrends",
                newName: "LossRatio");
        }
    }
}
