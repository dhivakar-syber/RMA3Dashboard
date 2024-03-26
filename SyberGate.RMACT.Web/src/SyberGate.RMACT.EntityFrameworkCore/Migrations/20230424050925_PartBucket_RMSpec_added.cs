using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class PartBucket_RMSpec_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartNumber",
                table: "PartBuckets");

            migrationBuilder.DropColumn(
                name: "PartNumber",
                table: "A3PartBuckets");

            migrationBuilder.AddColumn<string>(
                name: "RMSpec",
                table: "PartBuckets",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RMSpec",
                table: "A3PartBuckets",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RMSpec",
                table: "PartBuckets");

            migrationBuilder.DropColumn(
                name: "RMSpec",
                table: "A3PartBuckets");

            migrationBuilder.AddColumn<string>(
                name: "PartNumber",
                table: "PartBuckets",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PartNumber",
                table: "A3PartBuckets",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
