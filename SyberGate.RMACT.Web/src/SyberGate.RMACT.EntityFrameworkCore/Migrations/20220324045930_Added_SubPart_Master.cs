using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Added_SubPart_Master : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubParts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrossInputWeight = table.Column<decimal>(nullable: true),
                    CastingForgingWeight = table.Column<decimal>(nullable: true),
                    FinishedWeight = table.Column<decimal>(nullable: true),
                    ScrapRecoveryPercent = table.Column<double>(nullable: true),
                    PartNo = table.Column<string>(maxLength: 255, nullable: true),
                    ParentPartNo = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    ParentPartId = table.Column<int>(nullable: true),
                    SupplierId = table.Column<int>(nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    RMGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubParts_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubParts_Parts_ParentPartId",
                        column: x => x.ParentPartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubParts_RMGroups_RMGroupId",
                        column: x => x.RMGroupId,
                        principalTable: "RMGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubParts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubParts_BuyerId",
                table: "SubParts",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubParts_ParentPartId",
                table: "SubParts",
                column: "ParentPartId");

            migrationBuilder.CreateIndex(
                name: "IX_SubParts_RMGroupId",
                table: "SubParts",
                column: "RMGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SubParts_SupplierId",
                table: "SubParts",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubParts");
        }
    }
}
