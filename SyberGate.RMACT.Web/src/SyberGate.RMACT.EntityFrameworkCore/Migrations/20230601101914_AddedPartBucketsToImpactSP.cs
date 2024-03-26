using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class AddedPartBucketsToImpactSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BaseRMRate",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CuttingCost",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MOQVolume",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Others",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RMSurchargeGradeDiff",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RevBaseRMRate",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RevCuttingCost",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RevMOQVolume",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RevOthers",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RevRMSurchargeGradeDiff",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RevSecondaryProcessing",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RevSurfaceProtection",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RevThickness",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RevTransport",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SecondaryProcessing",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SurfaceProtection",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Thickness",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Transport",
                table: "A3PriceImpacts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseRMRate",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "CuttingCost",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "MOQVolume",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "Others",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RMSurchargeGradeDiff",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RevBaseRMRate",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RevCuttingCost",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RevMOQVolume",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RevOthers",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RevRMSurchargeGradeDiff",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RevSecondaryProcessing",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RevSurfaceProtection",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RevThickness",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "RevTransport",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "SecondaryProcessing",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "SurfaceProtection",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "Thickness",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "Transport",
                table: "A3PriceImpacts");
        }
    }
}
