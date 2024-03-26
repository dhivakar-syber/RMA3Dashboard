using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_Part4716 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Parts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_BuyerId",
                table: "Parts",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Buyers_BuyerId",
                table: "Parts",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Buyers_BuyerId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_BuyerId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Parts");
        }
    }
}
