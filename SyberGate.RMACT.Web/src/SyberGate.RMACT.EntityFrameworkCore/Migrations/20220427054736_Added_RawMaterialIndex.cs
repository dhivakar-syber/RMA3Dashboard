using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Added_RawMaterialIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RawMaterialIndexes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: true),
                    IndexNameId = table.Column<int>(nullable: false),
                    YearId = table.Column<int>(nullable: false),
                    RawMaterialGradeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterialIndexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RawMaterialIndexes_IndexNames_IndexNameId",
                        column: x => x.IndexNameId,
                        principalTable: "IndexNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RawMaterialIndexes_RawMaterialGrades_RawMaterialGradeId",
                        column: x => x.RawMaterialGradeId,
                        principalTable: "RawMaterialGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RawMaterialIndexes_Years_YearId",
                        column: x => x.YearId,
                        principalTable: "Years",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialIndexes_IndexNameId",
                table: "RawMaterialIndexes",
                column: "IndexNameId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialIndexes_RawMaterialGradeId",
                table: "RawMaterialIndexes",
                column: "RawMaterialGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialIndexes_YearId",
                table: "RawMaterialIndexes",
                column: "YearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawMaterialIndexes");
        }
    }
}
