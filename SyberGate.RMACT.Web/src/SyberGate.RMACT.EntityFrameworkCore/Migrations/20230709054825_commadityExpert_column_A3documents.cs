using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class commadityExpert_column_A3documents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "CommadityExpertEmailAddress",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CommadityExpertApproval",
                table: "A3Documents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CommadityExpertStatus",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommadityExpertToken",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommadityExpertremarks",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RCommadityExpertToken",
                table: "A3Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "CommadityExpertEmailAddress",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CommadityExpertApproval",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "CommadityExpertStatus",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "CommadityExpertToken",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "CommadityExpertremarks",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "RCommadityExpertToken",
                table: "A3Documents");
        }
    }
}
