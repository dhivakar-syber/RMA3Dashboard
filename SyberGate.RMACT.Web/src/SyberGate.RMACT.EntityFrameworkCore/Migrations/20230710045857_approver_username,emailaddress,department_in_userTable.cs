using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class approver_usernameemailaddressdepartment_in_userTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "CommadityExpertDepartment",
                table: "AbpUsers",
                nullable: true);

            

            migrationBuilder.AddColumn<string>(
                name: "CommadityExpertUserName",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpDepartment",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpUserName",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinDepartment",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinUserName",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "L4Department",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "L4UserName",
                table: "AbpUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            

            migrationBuilder.DropColumn(
                name: "CommadityExpertUserName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CpDepartment",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CpUserName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FinDepartment",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FinUserName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "L4Department",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "L4UserName",
                table: "AbpUsers");

            migrationBuilder.AddColumn<string>(
                name: "CommadityExpert",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
