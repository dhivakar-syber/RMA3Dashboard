using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Buyer_Supplier_ID_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "PartBuckets",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "PartBuckets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartBuckets_BuyerId",
                table: "PartBuckets",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartBuckets_SupplierId",
                table: "PartBuckets",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartBuckets_Buyers_BuyerId",
                table: "PartBuckets",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartBuckets_Suppliers_SupplierId",
                table: "PartBuckets",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(
            @"UPDATE PartBuckets
              SET BuyerId = (SELECT TOP 1 Id FROM Buyers WHERE Name = Buyer),
                  SupplierId = (SELECT TOP 1 Id FROM Suppliers WHERE Name = Supplier);"
            );

            migrationBuilder.Sql(
           @"delete from PartBuckets where isnull(SupplierId,0) = 0;"
           );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartBuckets_Buyers_BuyerId",
                table: "PartBuckets");

            migrationBuilder.DropForeignKey(
                name: "FK_PartBuckets_Suppliers_SupplierId",
                table: "PartBuckets");

            migrationBuilder.DropIndex(
                name: "IX_PartBuckets_BuyerId",
                table: "PartBuckets");

            migrationBuilder.DropIndex(
                name: "IX_PartBuckets_SupplierId",
                table: "PartBuckets");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "PartBuckets");

            migrationBuilder.DropColumn(
                name: "RMGroupId",
                table: "PartBuckets");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "PartBuckets");
        }
    }
}
