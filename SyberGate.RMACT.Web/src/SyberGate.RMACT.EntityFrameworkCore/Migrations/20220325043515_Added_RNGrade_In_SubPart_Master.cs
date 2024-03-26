using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Added_RNGrade_In_SubPart_Master : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RMGradeId",
                table: "SubParts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubParts_RMGradeId",
                table: "SubParts",
                column: "RMGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubParts_RMGrades_RMGradeId",
                table: "SubParts",
                column: "RMGradeId",
                principalTable: "RMGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubParts_RMGrades_RMGradeId",
                table: "SubParts");

            migrationBuilder.DropIndex(
                name: "IX_SubParts_RMGradeId",
                table: "SubParts");

            migrationBuilder.DropColumn(
                name: "RMGradeId",
                table: "SubParts");
        }
    }
}
