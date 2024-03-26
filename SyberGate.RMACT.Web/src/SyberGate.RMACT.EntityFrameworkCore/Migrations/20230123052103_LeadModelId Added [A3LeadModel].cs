using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class LeadModelIdAddedA3LeadModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeadModelId",
                table: "A3LeadModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_A3LeadModels_LeadModelId",
                table: "A3LeadModels",
                column: "LeadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_A3LeadModels_LeadModels_LeadModelId",
                table: "A3LeadModels",
                column: "LeadModelId",
                principalTable: "LeadModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_A3LeadModels_LeadModels_LeadModelId",
                table: "A3LeadModels");

            migrationBuilder.DropIndex(
                name: "IX_A3LeadModels_LeadModelId",
                table: "A3LeadModels");

            migrationBuilder.DropColumn(
                name: "LeadModelId",
                table: "A3LeadModels");
        }
    }
}
