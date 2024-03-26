using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class a3rmtaptooltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "A3RMTapTool",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocId = table.Column<int>(nullable: false),
                    RMGrade = table.Column<string>(maxLength: 255, nullable: true),
                    RMSpec = table.Column<string>(maxLength: 255, nullable: false),
                    Buyer = table.Column<string>(maxLength: 255, nullable: true),
                    Supplier = table.Column<string>(maxLength: 255, nullable: true),
                    BaseRMRate = table.Column<decimal>(nullable: false),
                    RMSurchargeGradeDiff = table.Column<decimal>(nullable: false),
                    SecondaryProcessing = table.Column<decimal>(nullable: false),
                    SurfaceProtection = table.Column<decimal>(nullable: false),
                    Thickness = table.Column<decimal>(nullable: false),
                    CuttingCost = table.Column<decimal>(nullable: false),
                    MOQVolume = table.Column<decimal>(nullable: false),
                    Transport = table.Column<decimal>(nullable: false),
                    Others = table.Column<decimal>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    SupplierId = table.Column<int>(nullable: true),
                    BuyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A3RMTapTool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_A3RMTapTool_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_A3RMTapTool_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_A3RMTapTool_BuyerId",
                table: "A3RMTapTool",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_A3RMTapTool_SupplierId",
                table: "A3RMTapTool",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "A3RMTapTool");
        }
    }
}
