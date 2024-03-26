using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Added_RawMaterialMixture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasMixture",
                table: "RMGroups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RawMaterialMixtures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RMGroupId = table.Column<int>(nullable: false),
                    RawMaterialGradeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterialMixtures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RawMaterialMixtures_RMGroups_RMGroupId",
                        column: x => x.RMGroupId,
                        principalTable: "RMGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RawMaterialMixtures_RawMaterialGrades_RawMaterialGradeId",
                        column: x => x.RawMaterialGradeId,
                        principalTable: "RawMaterialGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialMixtures_RMGroupId",
                table: "RawMaterialMixtures",
                column: "RMGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialMixtures_RawMaterialGradeId",
                table: "RawMaterialMixtures",
                column: "RawMaterialGradeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawMaterialMixtures");

            migrationBuilder.DropColumn(
                name: "HasMixture",
                table: "RMGroups");
        }
    }
}
