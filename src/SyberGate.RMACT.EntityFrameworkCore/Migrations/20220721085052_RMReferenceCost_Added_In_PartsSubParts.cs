using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class RMReferenceCost_Added_In_PartsSubParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RMReferenceCost",
                table: "SubParts",
                type: "decimal(18,5)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RMReferenceCost",
                table: "Parts",
                type: "decimal(18,5)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RMReferenceCost",
                table: "SubParts");

            migrationBuilder.DropColumn(
                name: "RMReferenceCost",
                table: "Parts");
        }
    }
}
