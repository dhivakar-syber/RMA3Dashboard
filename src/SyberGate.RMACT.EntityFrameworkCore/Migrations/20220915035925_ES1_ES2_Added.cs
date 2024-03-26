using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class ES1_ES2_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ES1",
                table: "GlobusData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ES2",
                table: "GlobusData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ES1",
                table: "A3SubPartImpacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ES2",
                table: "A3SubPartImpacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ES1",
                table: "A3PriceImpacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ES2",
                table: "A3PriceImpacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ES1",
                table: "GlobusData");

            migrationBuilder.DropColumn(
                name: "ES2",
                table: "GlobusData");

            migrationBuilder.DropColumn(
                name: "ES1",
                table: "A3SubPartImpacts");

            migrationBuilder.DropColumn(
                name: "ES2",
                table: "A3SubPartImpacts");

            migrationBuilder.DropColumn(
                name: "ES1",
                table: "A3PriceImpacts");

            migrationBuilder.DropColumn(
                name: "ES2",
                table: "A3PriceImpacts");
        }
    }
}
