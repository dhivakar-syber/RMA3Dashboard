using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class new_column_added_in_A3documents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CpApproval",
                table: "A3Documents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CpStatus",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpremarks",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FinApproval",
                table: "A3Documents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FinStatus",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Finremarks",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "L4Approval",
                table: "A3Documents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "L4Status",
                table: "A3Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "L4remarks",
                table: "A3Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpApproval",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "CpStatus",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "Cpremarks",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "FinApproval",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "FinStatus",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "Finremarks",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "L4Approval",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "L4Status",
                table: "A3Documents");

            migrationBuilder.DropColumn(
                name: "L4remarks",
                table: "A3Documents");
        }
    }
}
