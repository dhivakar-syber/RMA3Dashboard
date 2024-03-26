using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Added_RNGrade_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_RMGrades_RMGradeId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubParts_RMGrades_RMGradeId",
                table: "SubParts");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_RawMaterialGrades_RMGradeId",
                table: "Parts",
                column: "RMGradeId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubParts_RawMaterialGrades_RMGradeId",
                table: "SubParts",
                column: "RMGradeId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_RawMaterialGrades_RMGradeId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubParts_RawMaterialGrades_RMGradeId",
                table: "SubParts");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_RMGrades_RMGradeId",
                table: "Parts",
                column: "RMGradeId",
                principalTable: "RMGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubParts_RMGrades_RMGradeId",
                table: "SubParts",
                column: "RMGradeId",
                principalTable: "RMGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
