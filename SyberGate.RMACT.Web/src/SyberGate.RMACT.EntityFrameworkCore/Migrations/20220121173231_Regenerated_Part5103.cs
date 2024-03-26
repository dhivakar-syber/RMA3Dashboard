using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_Part5103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_RMGrades_RMGradeId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_RMGradeId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "RMGradeId",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "RawMaterialGradeId",
                table: "Parts",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_RawMaterialGrades_RawMaterialGradeId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_RawMaterialGradeId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "RawMaterialGradeId",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "RMGradeId",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_RMGradeId",
                table: "Parts",
                column: "RMGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_RMGrades_RMGradeId",
                table: "Parts",
                column: "RMGradeId",
                principalTable: "RMGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
