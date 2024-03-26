using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_BaseRMRate4843 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "BaseRMRates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "BaseRMRates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_BuyerId",
                table: "BaseRMRates",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_SupplierId",
                table: "BaseRMRates",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_Buyers_BuyerId",
                table: "BaseRMRates",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_Suppliers_SupplierId",
                table: "BaseRMRates",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseRMRates_Buyers_BuyerId",
                table: "BaseRMRates");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseRMRates_Suppliers_SupplierId",
                table: "BaseRMRates");

            migrationBuilder.DropIndex(
                name: "IX_BaseRMRates_BuyerId",
                table: "BaseRMRates");

            migrationBuilder.DropIndex(
                name: "IX_BaseRMRates_SupplierId",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "BaseRMRates");
        }
    }
}
