using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class userl4cpfin_emailaddress_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CpEmailAddress",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinEmailAddress",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "L4EmailAddress",
                table: "AbpUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpEmailAddress",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FinEmailAddress",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "L4EmailAddress",
                table: "AbpUsers");
        }
    }
}
