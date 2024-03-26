using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Added_BaseRMRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseRMRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseMonthYear = table.Column<string>(maxLength: 50, nullable: false),
                    UnitRate = table.Column<decimal>(nullable: false),
                    ScrapPercent = table.Column<double>(nullable: false),
                    RMGradeId = table.Column<int>(nullable: true),
                    UOMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseRMRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseRMRates_RMGrades_RMGradeId",
                        column: x => x.RMGradeId,
                        principalTable: "RMGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseRMRates_UOMs_UOMId",
                        column: x => x.UOMId,
                        principalTable: "UOMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_RMGradeId",
                table: "BaseRMRates",
                column: "RMGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_UOMId",
                table: "BaseRMRates",
                column: "UOMId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseRMRates");
        }
    }
}
