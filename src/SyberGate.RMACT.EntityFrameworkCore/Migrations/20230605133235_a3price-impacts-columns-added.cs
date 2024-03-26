using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class a3priceimpactscolumnsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"ALTER TABLE A3PriceImpacts
                     ADD SubMixture bit DEFAULT 0,
                         SubPart bit DEFAULT 0,
                         IsParentSubMixture bit DEFAULT 0,
                         IsParentMixture bit DEFAULT 0;"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
