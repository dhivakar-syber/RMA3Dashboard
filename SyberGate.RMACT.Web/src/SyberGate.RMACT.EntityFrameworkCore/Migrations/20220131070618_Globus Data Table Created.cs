using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class GlobusDataTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobusData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartNo = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    SupplierCode = table.Column<string>(nullable: true),
                    SuppliserName = table.Column<string>(nullable: true),
                    CurrentExwPrice = table.Column<decimal>(nullable: false),
                    PriceCurrency = table.Column<string>(nullable: true),
                    Uom = table.Column<string>(nullable: true),
                    Buyer = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    PackagingCost = table.Column<decimal>(nullable: false),
                    LogisticsCost = table.Column<decimal>(nullable: false),
                    PlantCode = table.Column<string>(nullable: true),
                    PlantDescription = table.Column<string>(nullable: true),
                    ContractNo = table.Column<string>(nullable: true),
                    SOB = table.Column<decimal>(nullable: false),
                    EPU = table.Column<decimal>(nullable: false),
                    SupplierId = table.Column<int>(nullable: true),
                    BuyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobusData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobusData_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlobusData_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlobusData_BuyerId",
                table: "GlobusData",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobusData_SupplierId",
                table: "GlobusData",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobusData");
        }
    }
}
