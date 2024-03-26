using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class newcolumns_token_a3documents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CpToken",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinToken",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "L4Token",
                table: "A3Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpToken",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "FinToken",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "L4Token",
                table: "A3Documents");
        }
    }
}
