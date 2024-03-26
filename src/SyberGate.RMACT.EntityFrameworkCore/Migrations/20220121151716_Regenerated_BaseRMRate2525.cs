using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_BaseRMRate2525 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseMonthYear",
                table: "BaseRMRates");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "BaseRMRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "BaseRMRates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_YearId",
                table: "BaseRMRates",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_Years_YearId",
                table: "BaseRMRates",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseRMRates_Years_YearId",
                table: "BaseRMRates");

            migrationBuilder.DropIndex(
                name: "IX_BaseRMRates_YearId",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "BaseRMRates");

            migrationBuilder.AddColumn<string>(
                name: "BaseMonthYear",
                table: "BaseRMRates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
