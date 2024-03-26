using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Create_A3Document : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "A3PriceImpacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocId = table.Column<int>(nullable: false),
                    slno = table.Column<int>(nullable: false),
                    PartNo = table.Column<string>(nullable: true),
                    RawMaterialGrade = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GrossInputWeight = table.Column<decimal>(nullable: true),
                    CastingForgingWeight = table.Column<decimal>(nullable: true),
                    FinishedWeight = table.Column<decimal>(nullable: true),
                    ScrapRecovery = table.Column<decimal>(nullable: true),
                    ScrapRecoveryPercent = table.Column<double>(nullable: true),
                    ScrapWeight = table.Column<decimal>(nullable: true),
                    CurrentRMCost = table.Column<decimal>(nullable: true),
                    RevisedRMCost = table.Column<decimal>(nullable: true),
                    OtherCost = table.Column<decimal>(nullable: true),
                    CurrentExwPrice = table.Column<decimal>(nullable: true),
                    RevisedExwPrice = table.Column<decimal>(nullable: true),
                    ExwPriceChangeInCost = table.Column<decimal>(nullable: true),
                    ExwPriceChangeInPer = table.Column<decimal>(nullable: true),
                    PackagingCost = table.Column<decimal>(nullable: true),
                    LogisticsCost = table.Column<decimal>(nullable: true),
                    CurrentFCAPrice = table.Column<decimal>(nullable: true),
                    RevisedFCAPrice = table.Column<decimal>(nullable: true),
                    CurrentAVOB = table.Column<decimal>(nullable: true),
                    RevisedAVOB = table.Column<decimal>(nullable: true),
                    PlantCode = table.Column<string>(nullable: true),
                    SOB = table.Column<decimal>(nullable: true),
                    GlobusEPU = table.Column<decimal>(nullable: true),
                    RMImpact = table.Column<decimal>(nullable: true),
                    IsParent = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A3PriceImpacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "A3PriceTrends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocId = table.Column<int>(nullable: false),
                    RevId = table.Column<int>(nullable: true),
                    RMGroupId = table.Column<int>(nullable: true),
                    RMGrade = table.Column<string>(nullable: true),
                    Uom = table.Column<string>(nullable: true),
                    SetteledMY = table.Column<string>(nullable: true),
                    SetteledUR = table.Column<decimal>(nullable: true),
                    RevisedMY = table.Column<string>(nullable: true),
                    RevisedUR = table.Column<decimal>(nullable: true),
                    BaseRMPOC = table.Column<string>(nullable: true),
                    ScrapSetteled = table.Column<decimal>(nullable: true),
                    ScrapRevised = table.Column<decimal>(nullable: true),
                    ScrapPOC = table.Column<string>(nullable: true),
                    RevApproved = table.Column<bool>(nullable: true),
                    SetApproved = table.Column<bool>(nullable: true),
                    SetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A3PriceTrends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "A3SubPartImpacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocId = table.Column<int>(nullable: false),
                    slno = table.Column<int>(nullable: true),
                    PartNo = table.Column<string>(nullable: true),
                    RawMaterialGrade = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GrossInputWeight = table.Column<decimal>(nullable: true),
                    CastingForgingWeight = table.Column<decimal>(nullable: true),
                    FinishedWeight = table.Column<decimal>(nullable: true),
                    ScrapRecovery = table.Column<decimal>(nullable: true),
                    ScrapRecoveryPercent = table.Column<double>(nullable: true),
                    ScrapWeight = table.Column<decimal>(nullable: true),
                    CurrentRMCost = table.Column<decimal>(nullable: true),
                    RevisedRMCost = table.Column<decimal>(nullable: true),
                    OtherCost = table.Column<decimal>(nullable: true),
                    CurrentExwPrice = table.Column<decimal>(nullable: true),
                    RevisedExwPrice = table.Column<decimal>(nullable: true),
                    ExwPriceChangeInCost = table.Column<decimal>(nullable: true),
                    ExwPriceChangeInPer = table.Column<decimal>(nullable: true),
                    PackagingCost = table.Column<decimal>(nullable: true),
                    LogisticsCost = table.Column<decimal>(nullable: true),
                    CurrentFCAPrice = table.Column<decimal>(nullable: true),
                    RevisedFCAPrice = table.Column<decimal>(nullable: true),
                    CurrentAVOB = table.Column<decimal>(nullable: true),
                    RevisedAVOB = table.Column<decimal>(nullable: true),
                    PlantCode = table.Column<string>(nullable: true),
                    SOB = table.Column<decimal>(nullable: true),
                    GlobusEPU = table.Column<decimal>(nullable: true),
                    RMImpact = table.Column<decimal>(nullable: true),
                    IsParent = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A3SubPartImpacts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "A3PriceImpacts");

            migrationBuilder.DropTable(
                name: "A3PriceTrends");

            migrationBuilder.DropTable(
                name: "A3SubPartImpacts");
        }
    }
}
