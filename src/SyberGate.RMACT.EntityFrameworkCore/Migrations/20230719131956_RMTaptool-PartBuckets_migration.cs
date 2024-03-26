using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class RMTaptoolPartBuckets_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
           @"select *  into #partbuct from partbuckets	where id in (select max(id) from PartBuckets   group by buyerid,supplierid,RMSpec,buckets,DATEFROMPARTS(Year, MONTH(Month + ' 01, 2000'), 01) ) order by DATEFROMPARTS(Year, MONTH(Month + ' 01, 2000'), 01),id

go
 
INSERT INTO rmtaptool (rmgrade, rmspec, buyer, supplier, BaseRMRate, RMSurchargeGradeDiff, SecondaryProcessing, SurfaceProtection, Thickness, CuttingCost, MOQVolume, Transport, Others, CreatedOn, Date, buyerid,SupplierId)
SELECT 
    '', rmspec, (select name from buyers where id=buyerid),(select name from suppliers where id=supplierid),
    MAX(CASE WHEN buckets = 'Base RM Rate' THEN value END),
    MAX(CASE WHEN buckets = 'RM Surcharge(Grade Diff)' THEN value END),
    MAX(CASE WHEN buckets = 'Secondary Processing' THEN value END),
    MAX(CASE WHEN buckets = 'Surface Protection' THEN value END),
    MAX(CASE WHEN buckets = 'Thickness' THEN value END),
    MAX(CASE WHEN buckets = 'Cutting Cost' THEN value END),
    MAX(CASE WHEN buckets = 'MOQ (Volume)' THEN value END),
    MAX(CASE WHEN buckets = 'Transport' THEN value END),
    MAX(CASE WHEN buckets = 'Others' THEN value END),
    DATEFROMPARTS(year, MONTH(DATEADD(MM, MONTH(month + ' 1, 2020') - 1, 0)), 1),
    DATEFROMPARTS(year, MONTH(DATEADD(MM, MONTH(month + ' 1, 2020') - 1, 0)), 1),
    buyerid, supplierid
FROM #partbuct
GROUP BY rmspec,month, year, buyerid, supplierid;"
           );


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
