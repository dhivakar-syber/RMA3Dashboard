using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_Part6231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_RawMaterialGrades_RawMaterialGradeId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_RawMaterialGradeId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "RMGroup",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "RawMaterialGradeId",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "RMGroupId",
                table: "Parts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_RMGroupId",
                table: "Parts",
                column: "RMGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_RMGroups_RMGroupId",
                table: "Parts",
                column: "RMGroupId",
                principalTable: "RMGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_RMGroups_RMGroupId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_RMGroupId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "RMGroupId",
                table: "Parts");

            migrationBuilder.AddColumn<string>(
                name: "RMGroup",
                table: "Parts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RawMaterialGradeId",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_RawMaterialGradeId",
                table: "Parts",
                column: "RawMaterialGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_RawMaterialGrades_RawMaterialGradeId",
                table: "Parts",
                column: "RawMaterialGradeId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
