using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_RawMaterialMixture7080 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "RawMaterialMixtures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "RawMaterialMixtures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "WeightRatio",
                table: "RawMaterialMixtures",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialMixtures_BuyerId",
                table: "RawMaterialMixtures",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialMixtures_SupplierId",
                table: "RawMaterialMixtures",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterialMixtures_Buyers_BuyerId",
                table: "RawMaterialMixtures",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterialMixtures_Suppliers_SupplierId",
                table: "RawMaterialMixtures",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterialMixtures_Buyers_BuyerId",
                table: "RawMaterialMixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterialMixtures_Suppliers_SupplierId",
                table: "RawMaterialMixtures");

            migrationBuilder.DropIndex(
                name: "IX_RawMaterialMixtures_BuyerId",
                table: "RawMaterialMixtures");

            migrationBuilder.DropIndex(
                name: "IX_RawMaterialMixtures_SupplierId",
                table: "RawMaterialMixtures");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "RawMaterialMixtures");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "RawMaterialMixtures");

            migrationBuilder.DropColumn(
                name: "WeightRatio",
                table: "RawMaterialMixtures");
        }
    }
}
