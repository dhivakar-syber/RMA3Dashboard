using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_RawMaterialGrade2232 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "RMGroupId",
            //    table: "RawMaterialGrades");

            migrationBuilder.AddColumn<bool>(
                name: "HasMixture",
                table: "RawMaterialGrades",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGroup",
                table: "RawMaterialGrades",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasMixture",
                table: "RawMaterialGrades");

            migrationBuilder.DropColumn(
                name: "IsGroup",
                table: "RawMaterialGrades");

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "RawMaterialGrades",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
