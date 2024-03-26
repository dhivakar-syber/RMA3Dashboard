using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Added_RNGrade_In_Part_Master : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RMGradeId",
                table: "Parts",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
