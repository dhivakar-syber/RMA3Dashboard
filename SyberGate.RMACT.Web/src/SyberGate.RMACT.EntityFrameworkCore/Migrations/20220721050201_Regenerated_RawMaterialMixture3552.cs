using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_RawMaterialMixture3552 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterialMixtures_RawMaterialGrades_RMGroupId",
                table: "RawMaterialMixtures");

            migrationBuilder.AddColumn<decimal>(
                name: "LossRatio",
                table: "RawMaterialMixtures",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterialMixtures_RMGroups_RMGroupId",
                table: "RawMaterialMixtures",
                column: "RMGroupId",
                principalTable: "RMGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterialMixtures_RMGroups_RMGroupId",
                table: "RawMaterialMixtures");

            migrationBuilder.DropColumn(
                name: "LossRatio",
                table: "RawMaterialMixtures");

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterialMixtures_RawMaterialGrades_RMGroupId",
                table: "RawMaterialMixtures",
                column: "RMGroupId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
