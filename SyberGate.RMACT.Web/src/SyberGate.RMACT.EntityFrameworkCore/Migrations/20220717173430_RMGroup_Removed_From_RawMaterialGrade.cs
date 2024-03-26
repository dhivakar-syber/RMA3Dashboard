using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class RMGroup_Removed_From_RawMaterialGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterialGrades_RMGroups_RMGroupId",
                table: "RawMaterialGrades");

            migrationBuilder.DropIndex(
                name: "IX_RawMaterialGrades_RMGroupId",
                table: "RawMaterialGrades");

            migrationBuilder.DropColumn(
                name: "RMGroupId",
                table: "RawMaterialGrades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RMGroupId",
                table: "RawMaterialGrades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialGrades_RMGroupId",
                table: "RawMaterialGrades",
                column: "RMGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterialGrades_RMGroups_RMGroupId",
                table: "RawMaterialGrades",
                column: "RMGroupId",
                principalTable: "RMGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
