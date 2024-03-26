using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class updatecostconversionspmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
         @"
ALTER procedure [dbo].[UpdateCostConversion]          
@SupplierId varchar(255) null,          
@BuyerId varchar(255) null,          
@Period varchar(255) null,          
@IsGenerateA3 bit,          
@A3Id int null          
AS          
          
--drop table #temp          
create table #temp ([Id] [int] IDENTITY(1,1) NOT NULL,          
 [ParentPartNo] [nvarchar](255) NULL,          
 [PartNo] [nvarchar](255) NULL,          
 [Description] [nvarchar](255) NOT NULL,          
 [RawMaterialGrade] [nvarchar](255) NULL,          
 [GrossInputWeight] [decimal](18, 5) NULL,          
 [CastingForgingWeight] [decimal](18, 5) NULL,          
 [FinishedWeight] [decimal](18, 5) NULL,          
 [ScrapRecovery] [decimal](18, 5) NULL,          
 [ScrapRecoveryPercent] [float] NULL,          
 [ScrapWeight] [decimal](18, 5) NULL,          
 [SetteledUnitRate] [decimal](18, 5) NULL,          
 [RevisedUnitRate] [decimal](18, 5) NULL,          
 [ScrapRateSetteled] [decimal](18, 5) NULL,          
 [ScrapRateRevised] [decimal](18, 5) NULL,          
 [CurrentRMCost] [decimal](18, 5) NULL,          
 [RevisedRMCost] [decimal](18, 5) NULL,          
 [OtherCost] [decimal](18, 5) NULL,          
 [CurrentExwPrice] [decimal](18, 5) NULL,          
 [RevisedExwPrice] [decimal](18, 5) NULL,          
 [ExwPriceChangeInCost] [decimal](18, 5) NULL,          
 [ExwPriceChangeInPer] [decimal](18, 5) NULL,          
 [PackagingCost] [decimal](18, 5) NULL,          
 [LogisticsCost] [decimal](18, 5) NULL,          
 [CurrentFCAPrice] [decimal](18, 5) NULL,          
 [RevisedFCAPrice] [decimal](18, 5) NULL,          
 [CurrentAVOB] [decimal](18, 5) NULL,          
 [RevisedAVOB] [decimal](18, 5) NULL,          
 [PlantCode] [nvarchar](255) NULL,          
 [SOB] [varchar](10) NULL,          
 [GlobusEPU] [decimal](18, 5) NULL,          
 [RMImpact] [decimal](18, 6) NULL,          
 [SupplierId] [int] NULL,          
 [BuyerId] [int] NULL,          
 [IsParent] [Bit],          
 [RMGrade] [nvarchar](255) NULL,          
 [ConversionCost] [decimal](18, 6) NULL,          
 [RMRerfPrice] [decimal](18, 6) NULL,          
 [CurrentCostPer] [decimal](18, 6) NULL,          
 [RevisedCostPer] [decimal](18, 6) NULL          
 )          
          
declare @startdate date          
declare @enddate date          
set @startdate = convert(date,convert(varchar, SUBSTRING(@Period, 1, 10) , 101),101)          
set @enddate = convert(date,convert(varchar, SUBSTRING(@Period, 13, 11) , 101),101)          
          
insert #temp ([PartNo], [Description],  [RawMaterialGrade], [GrossInputWeight], [CastingForgingWeight], [FinishedWeight], [ScrapRecoveryPercent], [SetteledUnitRate], [RevisedUnitRate], [ScrapRateSetteled],          
 [ScrapRateRevised], [CurrentExwPrice], [PackagingCost], [LogisticsCost], [PlantCode], [SOB], [GlobusEPU], [BuyerId], [SupplierId], [IsParent], RMGrade, [ConversionCost], [RMRerfPrice], [CurrentCostPer], [RevisedCostPer]  )          
select p.PartNo, p.Description, br.RawMaterialGrade, p.GrossInputWeight, p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent, br.SetteledUnitRate, br.RevisedUnitRate, br.SetteledScrapPer,           
br.RevisedScrapPer, gd.CurrentExwPrice, isnull(gd.PackagingCost,0), isnull(gd.LogisticsCost,0), gd.PlantCode, cast(gd.SOB as float), gd.EPU, p.BuyerId, p.SupplierId, case when isnull(p.IsParent, 0) = 0 then 0 else 1 end, grd.Name RMGrade,          
ISNULL(p.ConversionCost,0), ISNULL(p.RMReferenceCost,0), 0, 0          
from parts p           
inner join RawMaterialGrades g on g.id = p.RMGroupid          
inner join RawMaterialGrades grd on grd.Id = p.RMGradeId          
inner join vw_RMTrend br on br.Id = (select max(b.Id) from vw_rmtrend b where br.rmgroupid = b.rmgroupid and b.BuyerId = br.BuyerId and b.SupplierId = br.SupplierId          
 and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)))           
 and g.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId          left outer join GlobusData gd on gd.PartNo = p.PartNo and gd.id =           
 (select top 1 d.Id from GlobusData d where d.PartNo = gd.PartNo and d.Status in ('Auto Confirmed', 'Released', 'Confirmed')           
  and (d.FromDate < @startdate or d.FromDate < @enddate) --and (d.ToDate >= @startdate or d.ToDate >= @enddate)          
 and d.BuyerId = gd.BuyerId and gd.SupplierId = d.SupplierId and gd.PlantCode = d.plantcode order by FromDate desc) and  p.BuyerId = gd.BuyerId and gd.SupplierId = p.SupplierId          
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId and g.HasMixture = 0           
          
          
insert #temp ([PartNo], [Description],  [RawMaterialGrade], [GrossInputWeight], [CastingForgingWeight], [FinishedWeight], [ScrapRecoveryPercent], [SetteledUnitRate], [RevisedUnitRate], [ScrapRateSetteled],          
 [ScrapRateRevised], [CurrentExwPrice], [PackagingCost], [LogisticsCost], [PlantCode], [SOB], [GlobusEPU], [BuyerId], [SupplierId], [IsParent], RMGrade, [ConversionCost], [RMRerfPrice], [CurrentCostPer], [RevisedCostPer])          
select p.PartNo, p.Description, grd.Name, p.GrossInputWeight, p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent,           
sum( isnull(nullif(br.swRatio,0),100) * 0.01 * isnull(br.SetteledUnitRate,0) * isnull(nullif(br.slRatio,0),1)) SetteledUnitRate,           
sum( isnull(nullif(br.rwRatio,0),100) * 0.01 * isnull(br.RevisedUnitRate,0) * isnull(nullif(br.rlRatio,0),1))  RevisedUnitRate, sum(isnull(br.SetteledScrapPer,0)),           
sum(isnull(br.RevisedScrapPer,0)), gd.CurrentExwPrice, isnull(gd.PackagingCost,0), isnull(gd.LogisticsCost,0), gd.PlantCode, cast(gd.SOB as float), gd.EPU, p.BuyerId, p.SupplierId, case when isnull(p.IsParent, 0) = 0 then 0 else 1 end, grd.Name RMGrade,  
  
    
      
        
         
max(isnull(p.ConversionCost,0)), max(isnull(p.RMReferenceCost,0)), 0, 0          
from parts p           
inner join RawMaterialMixtures m on m.SupplierId = p.SupplierId and m.BuyerId = p.BuyerId and m.RMGroupId = p.RMGradeId          
inner join RawMaterialGrades g on g.id = m.RawMaterialGradeId          
inner join RawMaterialGrades grd on grd.Id = p.RMGradeId          
inner join vw_RMTrend br on br.Id = (select max(b.Id) from vw_rmtrend b where br.rmgroupid = b.rmgroupid and b.BuyerId = br.BuyerId and b.SupplierId = br.SupplierId          
 and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)))           
 and g.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId          
left outer join GlobusData gd on gd.PartNo = p.PartNo and gd.id =           
 (select top 1 d.Id from GlobusData d where d.PartNo = gd.PartNo and d.Status in ('Auto Confirmed', 'Released', 'Confirmed')           
  and (d.FromDate < @startdate or d.FromDate < @enddate) --and (d.ToDate >= @startdate or d.ToDate >= @enddate)          
 and d.BuyerId = gd.BuyerId and gd.SupplierId = d.SupplierId and gd.PlantCode = d.plantcode order by FromDate desc) and  p.BuyerId = gd.BuyerId and gd.SupplierId = p.SupplierId          
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId and grd.HasMixture = 1          
group by P.Id, p.PartNo, p.Description, grd.Name, p.GrossInputWeight, p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent,            
 gd.CurrentExwPrice, isnull(gd.PackagingCost,0), isnull(gd.LogisticsCost,0), gd.PlantCode, cast(gd.SOB as float), gd.EPU, p.BuyerId, p.SupplierId, isnull(p.IsParent, 0), grd.Name           
order by p.Id          
          
insert #temp ([ParentPartNo], [PartNo], [Description],  [RawMaterialGrade], [GrossInputWeight], [CastingForgingWeight], [FinishedWeight], [ScrapRecoveryPercent], [SetteledUnitRate], [RevisedUnitRate], [ScrapRateSetteled],          
 [ScrapRateRevised], [CurrentExwPrice], [PackagingCost], [LogisticsCost], [PlantCode], [SOB], [GlobusEPU], [BuyerId], [SupplierId], [IsParent], RMGrade, [ConversionCost], [RMRerfPrice], [CurrentCostPer], [RevisedCostPer] )          
select p.ParentPartNo, p.PartNo, p.Description, g.Name RawMaterialGrade, p.GrossInputWeight, p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent, br.SetteledUnitRate, br.RevisedUnitRate, br.SetteledScrapPer,           
br.RevisedScrapPer, gd.CurrentExwPrice, isnull(gd.PackagingCost,0), isnull(gd.LogisticsCost,0), gd.PlantCode, cast(gd.SOB as float), gd.EPU, p.BuyerId, p.SupplierId, 0, grd.Name RMGrade,          
ISNULL(p.ConversionCost,0), ISNULL(p.RMReferenceCost,0), 0, 0          
from SubParts p           
inner join #temp pp on pp.PartNo = p.ParentPartNo          
inner join RawMaterialGrades g on g.id = p.RMGroupid          
inner join RawMaterialGrades grd on grd.Id = p.RMGradeId          
inner join vw_RMTrend br on br.Id = (select max(b.Id) from vw_rmtrend b where br.rmgroupid = b.rmgroupid and b.BuyerId = br.BuyerId and b.SupplierId = br.SupplierId          
 and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)))           
 and g.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId          
left outer join GlobusData gd on gd.PartNo = p.PartNo and gd.Id =           
 (select top 1 d.Id from GlobusData d where d.PartNo = gd.PartNo and d.Status in ('Auto Confirmed', 'Released', 'Confirmed')           
  and (d.FromDate < @startdate or d.FromDate < @enddate) --and (d.ToDate >= @startdate or d.ToDate >= @enddate)          
 and d.BuyerId = gd.BuyerId and gd.SupplierId = d.SupplierId and gd.PlantCode = d.PlantCode order by FromDate desc) and gd.PlantCode = pp.PlantCode and p.BuyerId = gd.BuyerId and gd.SupplierId = p.SupplierId          
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId          
      
select p.ParentPartNo, p.PartNo, p.Description, grd.Name, p.GrossInputWeight, p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent,           
sum( isnull(nullif(br.swRatio,0),100) * 0.01 * isnull(br.SetteledUnitRate,0) * isnull(nullif(br.slRatio,0),1)) SetteledUnitRate,           
sum( isnull(nullif(br.rwRatio,0),100) * 0.01 * isnull(br.RevisedUnitRate,0) * isnull(nullif(br.rlRatio,0),1))  RevisedUnitRate, sum(isnull(br.SetteledScrapPer,0)) [ScrapRateSetteled],           
sum(isnull(br.RevisedScrapPer,0)) [ScrapRateRevised], gd.CurrentExwPrice [CurrentExwPrice], isnull(gd.PackagingCost,0) [PackagingCost], isnull(gd.LogisticsCost,0) [LogisticsCost], gd.PlantCode,       
cast(gd.SOB as float) [SOB], gd.EPU, p.BuyerId, p.SupplierId, 1 [IsParent], grd.Name RMGrade,        
max(isnull(p.ConversionCost,0)) [ConversionCost], max(isnull(p.RMReferenceCost,0)) [RMRerfPrice], 0 [CurrentCostPer], 0  [RevisedCostPer]      
into #submixtureparent      
from SubParts p           
inner join RawMaterialMixtures m on m.SupplierId = p.SupplierId and m.BuyerId = p.BuyerId and m.RMGroupId = p.RMGradeId          
inner join RawMaterialGrades g on g.id = m.RawMaterialGradeId          
inner join RawMaterialGrades grd on grd.Id = p.RMGradeId          
inner join vw_RMTrend br on br.Id = (select max(b.Id) from vw_rmtrend b where br.rmgroupid = b.rmgroupid and b.BuyerId = br.BuyerId and b.SupplierId = br.SupplierId          
  and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)))           
 and g.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId          
left outer join GlobusData gd on gd.PartNo = p.PartNo and gd.id =           
 (select top 1 d.Id from GlobusData d where d.PartNo = gd.PartNo and d.Status in ('Auto Confirmed', 'Released', 'Confirmed')           
  and (d.FromDate < @startdate or d.FromDate < @enddate)        
 and d.BuyerId = gd.BuyerId and gd.SupplierId = d.SupplierId and gd.PlantCode = d.plantcode order by FromDate desc) and  p.BuyerId = gd.BuyerId and gd.SupplierId = p.SupplierId          
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId and grd.HasMixture = 1          
group by P.Id, p.ParentPartNo, p.PartNo, p.Description, grd.Name, p.GrossInputWeight, p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent,            
 gd.CurrentExwPrice, isnull(gd.PackagingCost,0), isnull(gd.LogisticsCost,0), gd.PlantCode, cast(gd.SOB as float), gd.EPU, p.BuyerId, p.SupplierId, grd.Name           
order by p.Id             
          
          
update p set p.ConversionCost = isnull((t.RMRerfPrice - t.SetteledUnitRate),0) from #temp t inner join Parts p on t.PartNo = p.PartNo and t.SupplierId = p.SupplierId and p.BuyerId = t.BuyerId where RMRerfPrice > 0  and t.ConversionCost = 0          
update p set p.ConversionCost = isnull((t.RMRerfPrice - t.SetteledUnitRate),0) from #submixtureparent t inner join SubParts p on p.ParentPartNo = t.ParentPartNo and t.PartNo = p.PartNo and t.SupplierId = p.SupplierId and p.BuyerId = t.BuyerId where RMRerfPrice > 0  and t.ConversionCost = 0   
    
             
          
select top 1 * from #temp          
--------------------------------------------------------------EOP-------------------------------------------------------- 
GO







");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
