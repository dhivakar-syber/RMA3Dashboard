using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class ColumnsAddedInA3PriceImpacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsParentMixture",
                table: "A3PriceImpacts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsParentSubMixture",
                table: "A3PriceImpacts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SubMixture",
                table: "A3PriceImpacts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SubPart",
                table: "A3PriceImpacts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsParentMixture",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "IsParentSubMixture",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "SubMixture",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "SubPart",
                table: "A3PriceImpacts");
        }
    }
}
