using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class RMTapToolTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RMTapTool",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RMGrade = table.Column<string>(maxLength: 30, nullable: true),
                    RMSpec = table.Column<string>(maxLength: 30, nullable: false),
                    Buyer = table.Column<string>(maxLength: 30, nullable: true),
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
                    table.PrimaryKey("PK_RMTapTool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RMTapTool_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RMTapTool_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RMTapTool_BuyerId",
                table: "RMTapTool",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_RMTapTool_SupplierId",
                table: "RMTapTool",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RMTapTool");
        }
    }
}
