using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class file_namefilebyte_in_supportattachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "SupportAttachments",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Filebyte",
                table: "SupportAttachments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "SupportAttachments");

            migrationBuilder.DropColumn(
                name: "Filebyte",
                table: "SupportAttachments");
        }
    }
}
