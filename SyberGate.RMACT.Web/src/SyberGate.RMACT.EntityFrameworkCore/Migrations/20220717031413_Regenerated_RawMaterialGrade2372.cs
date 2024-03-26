using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_RawMaterialGrade2372 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RawMaterialGradeId",
                table: "RawMaterialGrades",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialGrades_RawMaterialGradeId",
                table: "RawMaterialGrades",
                column: "RawMaterialGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterialGrades_RawMaterialGrades_RawMaterialGradeId",
                table: "RawMaterialGrades",
                column: "RawMaterialGradeId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterialGrades_RawMaterialGrades_RawMaterialGradeId",
                table: "RawMaterialGrades");

            migrationBuilder.DropIndex(
                name: "IX_RawMaterialGrades_RawMaterialGradeId",
                table: "RawMaterialGrades");

            migrationBuilder.DropColumn(
                name: "RawMaterialGradeId",
                table: "RawMaterialGrades");
        }
    }
}
