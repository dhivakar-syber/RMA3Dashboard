using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Weight_Loss_ratio_AddedIn_BaseRMRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LossRatio",
                table: "BaseRMRates",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WeightRatio",
                table: "BaseRMRates",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LossRatio",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "WeightRatio",
                table: "BaseRMRates");
        }
    }
}
