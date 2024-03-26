using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Added_RMReference_Part_Import : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RMReference",
                table: "SubParts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RMReference",
                table: "Parts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RMReference",
                table: "SubParts");

            migrationBuilder.DropColumn(
                name: "RMReference",
                table: "Parts");
        }
    }
}
