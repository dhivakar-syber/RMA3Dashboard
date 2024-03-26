using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class rejection_token_added_in_a3document : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RCpToken",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RFinToken",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RL4Token",
                table: "A3Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RCpToken",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "RFinToken",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "RL4Token",
                table: "A3Documents");
        }
    }
}
