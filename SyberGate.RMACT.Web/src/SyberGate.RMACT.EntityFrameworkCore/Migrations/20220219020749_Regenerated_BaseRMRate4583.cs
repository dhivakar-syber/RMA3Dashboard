using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_BaseRMRate4583 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseRMRates_RawMaterialGrades_RawMaterialGradeId",
                table: "BaseRMRates");

            migrationBuilder.DropIndex(
                name: "IX_BaseRMRates_RawMaterialGradeId",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "RawMaterialGradeId",
                table: "BaseRMRates");

            migrationBuilder.AddColumn<int>(
                name: "RMGroupId",
                table: "BaseRMRates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_RMGroupId",
                table: "BaseRMRates",
                column: "RMGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_RMGroups_RMGroupId",
                table: "BaseRMRates",
                column: "RMGroupId",
                principalTable: "RMGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseRMRates_RMGroups_RMGroupId",
                table: "BaseRMRates");

            migrationBuilder.DropIndex(
                name: "IX_BaseRMRates_RMGroupId",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "RMGroupId",
                table: "BaseRMRates");

            migrationBuilder.AddColumn<int>(
                name: "RawMaterialGradeId",
                table: "BaseRMRates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_RawMaterialGradeId",
                table: "BaseRMRates",
                column: "RawMaterialGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_RawMaterialGrades_RawMaterialGradeId",
                table: "BaseRMRates",
                column: "RawMaterialGradeId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
