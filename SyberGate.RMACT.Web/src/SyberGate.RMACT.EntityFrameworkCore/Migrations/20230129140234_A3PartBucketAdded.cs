using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class A3PartBucketAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "A3PartBuckets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocId = table.Column<int>(nullable: false),
                    PartNumber = table.Column<string>(maxLength: 30, nullable: false),
                    Buckets = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    Buyer = table.Column<string>(maxLength: 30, nullable: true),
                    Supplier = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A3PartBuckets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "A3PartBuckets");
        }
    }
}
