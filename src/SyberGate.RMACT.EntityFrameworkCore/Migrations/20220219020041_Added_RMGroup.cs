using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Added_RMGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RMGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RMGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplierBuyerMap",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 50, nullable: false),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: false),
                    SupplierCode = table.Column<string>(maxLength: 50, nullable: false),
                    SupplierName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierBuyerMap", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RMGroups");

            migrationBuilder.DropTable(
                name: "SupplierBuyerMap");
        }
    }
}
