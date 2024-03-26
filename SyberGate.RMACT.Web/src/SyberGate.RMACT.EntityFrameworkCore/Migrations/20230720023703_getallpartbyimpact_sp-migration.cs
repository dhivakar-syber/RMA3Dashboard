﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class getallpartbyimpact_spmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
           @"ALTER procedure [dbo].[GetAllPartByImpact]                      
@SupplierId varchar(255) null,                      
@BuyerId varchar(255) null,                      
@Period varchar(255) null,                      
@IsGenerateA3 bit,                      
@A3Id int null,                      
@SpecId varchar(255) null,   
@GradeId varchar(255) null, 
@Plant varchar(255) null                  
AS                      
                      
--drop table #temp                      
create table #temp ([Id] [int] IDENTITY(1,1) NOT NULL,          
               [slNo] [nvarchar](10)  NULL,    
      [pslno] [nvarchar](255)   NULL,    
    [subslNo] [nvarchar](255)   NULL,    
    [mixslNo] [nvarchar](255)   NULL,    
               [PartNo] [nvarchar](255) NULL,                    
               [Description] [nvarchar](255) NOT NULL,                    
               [ES1] [nvarchar](255) NULL,                
               [ES2] [nvarchar](255) NULL,                
               [RawMaterialGrade] [nvarchar](255) NULL,                 
               [RawMaterialgroup] [nvarchar](255) NULL,                            
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
               [Delta][decimal](18, 5) NULL,                      
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
               [RevisedCostPer] [decimal](18, 6) NULL,                
               [RMReference] [nvarchar](255) NULL,                            
               [settledDate] [Date] NULL,                            
               [RevisedDate] [Date] NULL,     
               [IsSubMixture] [Bit],                    
               [ParentPartNo] [nvarchar](255) NULL,                    
              [IsSubPart] [Bit],                    
               [RMgrdId][int] NULL,                  
               [isparentSubMixture] [Bit],                  
               [isparentMixture] [Bit],                
               [MixtureUnitRateS] [decimal](18, 5) NULL,                 
    [MixtureUnitRateR] [decimal](18, 5) NULL,                
    SBaseRMRate [decimal](18, 5) NULL,                
    SRMSurchargeGradeDiff [decimal](18, 5) NULL,                
    SSecondaryProcessing [decimal](18, 5) NULL,                
    SSurfaceProtection [decimal](18, 5) NULL,                
    SThickness [decimal](18, 5) NULL,                
    SCuttingCost [decimal](18, 5) NULL,                
    SMOQVolume [decimal](18, 5) NULL,                
    STransport [decimal](18, 5) NULL,                
    SOthers [decimal](18, 5) NULL,                
    RBaseRMRate [decimal](18, 5) NULL,                
    RRMSurchargeGradeDiff [decimal](18, 5) NULL,                
    RSecondaryProcessing [decimal](18, 5) NULL,                
    RSurfaceProtection [decimal](18, 5) NULL,                
    RThickness [decimal](18, 5) NULL,                
    RCuttingCost [decimal](18, 5) NULL,                
    RMOQVolume [decimal](18, 5) NULL,                
    RTransport [decimal](18, 5) NULL,                
    ROthers [decimal](18, 5) NULL,                
    ProcessImpactTotal [decimal](18, 5) NULL,                
    RMImpactTotal [decimal](18, 5) NULL                
)                             
                        
 CREATE TABLE #PlantTable (Code varchar(255));                    
 INSERT INTO #PlantTable (Code) SELECT value FROM STRING_SPLIT(@Plant, ',');                    
                      
                    
declare @startdate date                        
declare @enddate date                      
set @startdate = convert(date,convert(varchar, SUBSTRING(@Period, 1, 10) , 101),101)                       
set @enddate = convert(date,convert(varchar, SUBSTRING(@Period, 13, 11) , 101),101)                     
                  
--------------------------------------------------------------------------------------------------------------------------------------------                      
--straight case                
                
insert #temp (slno, PartNo,[Description], [ES1], [ES2], [RawMaterialgroup],[RawMaterialGrade], [GrossInputWeight], [CastingForgingWeight], [FinishedWeight], [ScrapRecoveryPercent], [SetteledUnitRate], [RevisedUnitRate], [ScrapRateSetteled],              
  
       [ScrapRateRevised], [CurrentExwPrice], [PackagingCost], [LogisticsCost], [PlantCode], [SOB], [GlobusEPU], [BuyerId], [SupplierId], [IsParent], RMGrade, [ConversionCost], [RMRerfPrice], [CurrentCostPer], [RevisedCostPer], [RMReference]              
  
   ,[settledDate],[RevisedDate]  ,[ParentPartNo],[RMgrdId],IsSubPart,isparentSubMixture,IsSubMixture,isparentMixture                 
      )                 
                 
select cast(ROW_NUMBER() OVER (ORDER BY p.id) as nvarchar(255))slno,p.PartNo, p.Description, gd.ES1, gd.ES2, g.Name,br.RawMaterialGrade, p.GrossInputWeight, p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent,    
br.SetteledUnitRate, br.RevisedUnitRate, br.SetteledScrapPer,                    
 br.RevisedScrapPer, gd.CurrentExwPrice, isnull(gd.PackagingCost,0), isnull(gd.LogisticsCost,0), gd.PlantCode, cast(gd.SOB as float), gd.EPU, p.BuyerId, p.SupplierId, case when isnull(p.IsParent, 0) = 0 then 0 else 1 end, grd.Name RMGrade,                
  
 ISNULL(p.ConversionCost,0), ISNULL(p.RMReferenceCost,0), br.SetteledUnitRate, br.RevisedUnitRate, p.RMReference                    
,br.settleddate,br.reviseddate , p.PartNo,grd.id,0,0,0,0                  
from parts p                       
left outer join RawMaterialGrades g on g.id = p.RMGroupid                    
left outer join RawMaterialGrades grd on grd.Id = p.RMGradeId                 
left outer join vw_RMTrend br on br.Id = (select top 1 (b.Id) from vw_rmtrend b where br.rmgroupid = b.rmgroupid and b.BuyerId = br.BuyerId and b.SupplierId = br.SupplierId           
 and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)) order by b.reviseddate desc)                             
 --and g.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId                             
       and grd.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId                                 
left outer join GlobusData gd on gd.PartNo = p.PartNo and gd.id =                 
 (select top 1 d.Id from GlobusData d where d.PartNo = gd.PartNo and d.Status in ('Auto Confirmed', 'Released', 'Confirmed')                
  and (d.FromDate < @startdate or d.FromDate < @enddate) --and (d.ToDate >= @startdate or d.ToDate >= @enddate)                        
    and d.BuyerId = gd.BuyerId and gd.SupplierId = d.SupplierId and gd.PlantCode = d.plantcode and isnull(gd.ES1,'') = isnull(d.ES1, '') and isnull(gd.ES2, '') = isnull(d.ES2, '') order by  FromDate  desc, id desc)                 
 and  p.BuyerId = gd.BuyerId and gd.SupplierId = p.SupplierId                           
    and gd.plantcode in (SELECT code FROM Plants WHERE @plant IS NULL UNION ALL SELECT code FROM #PlantTable WHERE @plant IS NOT NULL)                  
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId and isnull(grd.HasMixture, 0) = 0                     
 --and case when isnull(@GroupId, 0) = 0 then 1 else @GroupId end = case when isnull(@GroupId, 0) = 0 then 1 else g.Id end 
 and case when isnull(@GradeId, 0) = 0 then 1 else @GradeId end = case when isnull(@GradeId, 0) = 0 then 1 else g.Id end
 and case when isnull(@SpecId, 0) = 0 then 1 else @SpecId end = case when isnull(@SpecId, 0) = 0 then 1 else grd.Id end
   and gd.plantcode in (SELECT code FROM Plants WHERE @plant IS NULL UNION ALL SELECT code FROM #PlantTable WHERE @plant IS NOT NULL)                       
           
  declare @hasid int  
  select @hasid = max(id) from #temp          
            
 ---------------------------------<<<<<<<<                    
 --------------------------------->>>>>>>>                    
 --SubPart Impact                
select slno,p.Id as id,p.PartNo, p.Description, pt.ES1, pt.ES2, g.[Name] rawmaterialGrade,grp.[Name] Rawmaterialgroup,                     
p.GrossInputWeight,                    
p.CastingForgingWeight,                    
p.FinishedWeight,                 
p.ScrapRecoveryPercent,                      
isnull(br.SetteledUnitRate,0) SetteledUnitRate,                
isnull(br.RevisedUnitRate,0) RevisedUnitRate,                
cast(0 as decimal(18,5)) MixtureUnitRateS,                
cast(0 as decimal(18,5)) MixtureUnitRateR,                
(isnull(br.SetteledScrapPer,0)) SetteledScrapPer,                     
(isnull(br.RevisedScrapPer,0)) RevisedScrapPer, pt.CurrentExwPrice, (isnull(pt.PackagingCost,0))PackagingCost, (isnull(pt.LogisticsCost,0)) LogisticsCost, pt.PlantCode, (isnull(cast(pt.SOB as float),0)) SOB,                     
pt.GlobusEPU EPU, p.BuyerId, p.SupplierId, 0 IsParent,cast(0.00 as decimal(18,5)) [OtherCost],                            
g.Name RMGrade,                           
(isnull(p.ConversionCost,0)) ConversionCost, (isnull(p.RMReferenceCost,0)) RMReferenceCost, cast(0.00 as decimal(18,5))  [CurrentCostPer], cast(0.00 as decimal(18,5)) [RevisedCostPer], p.RMReference ,br.settledDate,br.RevisedDate ,                        
cast(0.00 as decimal(18,5)) [ScrapRecovery], cast(0.00 as decimal(18,5)) [ScrapWeight] , cast(0.00 as decimal(18,5)) [CurrentRMCost], cast(0.00 as decimal(18,5))   [RevisedRMCost],                     
br.SetteledScrapPer ScrapRateSetteled,                    
br.RevisedScrapPer ScrapRateRevised,                    
cast(0.00 as decimal(18,5)) [Delta], cast(0.00 as decimal(18,5)) [RevisedExwPrice] ,0 [Issubmixture], pt.PartNo [ParentPartNo],1 IsSubPart,pt.RMgrdId,0 isparentSubMixture,0 isparentmixture                 
 into #hasSubPart                       
from #temp pt                     
inner join SubParts p on p.ParentPartNo = pt.PartNo and pt.SupplierId = p.SupplierId and pt.BuyerId = p.BuyerId                    
--left outer join RawMaterialMixtures m on m.SupplierId = p.SupplierId and m.BuyerId = p.BuyerId and m.RMGroupId = p.RMGradeId                    
left outer join RawMaterialGrades g on g.id = p.RMGradeId                   
left outer join RawMaterialGrades grp on grp.id = p.RMGroupId                          
--left outer join RawMaterialGrades grd on grd.Id = p.RMGradeId                    
left outer join vw_RMTrend br on br.Id = (select top 1 (b.Id) from vw_rmtrend b where br.rmgroupid = b.rmgroupid and b.BuyerId = br.BuyerId and b.SupplierId = br.SupplierId                             
  and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)) order by b.reviseddate desc)                           
    and g.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId                    
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId                       
-- to be rewrite                    
 --and case when isnull(@GroupId, 0) = 0 then 1 else @GroupId end = case when isnull(@GroupId, 0) = 0 then 1 else g.Id end
 and case when isnull(@GradeId, 0) = 0 then 1 else @GradeId end = case when isnull(@GradeId, 0) = 0 then 1 else grp.Id end
 and case when isnull(@SpecId, 0) = 0 then 1 else @SpecId end = case when isnull(@SpecId, 0) = 0 then 1 else g.Id end
 and pt.PlantCode in (SELECT code  FROM Plants  WHERE @plant IS NULL  UNION ALL  SELECT code  FROM #PlantTable  WHERE @plant IS NOT NULL)                  
 and isnull(g.HasMixture, 0) = 0                  
order by p.Id                      
                
update #hasSubPart set [ScrapRecovery] = isnull([GrossInputWeight],0) - isnull([FinishedWeight],0)                           
update #hasSubPart set [ScrapWeight] = isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)                      
update #hasSubPart set [CurrentRMCost] = Round((isnull([GrossInputWeight],0) * isnull([SetteledUnitRate],0)) - (isnull([ScrapRateSetteled],0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2),                   
      [RevisedRMCost] = Round((isnull([RevisedUnitRate],0) * isnull([GrossInputWeight],0)) - (isnull([ScrapRateRevised],0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2)                  
update #hasSubPart set  [OtherCost] = isnull([CurrentExwPrice],0) - isnull([CurrentRMCost],0)                     
update #hasSubPart set Delta = (isnull(RevisedRMCost,0) - isnull(CurrentRMCost,0))                        
                    
                  
 ----------------------------------------------------------------------                  
 ---subParentMixture                  
                  
 insert #hasSubPart (id,[PartNo], [Description], [ES1], [ES2], [RawMaterialgroup], [GrossInputWeight], [CastingForgingWeight], [FinishedWeight], [ScrapRecoveryPercent],           
 [SetteledUnitRate], [RevisedUnitRate], MixtureUnitRateS, MixtureUnitRateR, SetteledScrapPer,                  
   RevisedScrapPer, [CurrentExwPrice], [PackagingCost], [LogisticsCost], [PlantCode], [SOB], EPU, [BuyerId], [SupplierId], [IsParent], RMGrade, [ConversionCost], RMReferenceCost, [CurrentCostPer], [RevisedCostPer], [RMReference]                           
 
   
  ,[settledDate],[RevisedDate]                            
  ,ParentPartNo,IsParentSubMixture,IsSubPart,IsSubMixture, [OtherCost],isparentmixture                  
      )                  
select p.id as id,p.PartNo, p.Description, gd.ES1, gd.ES2, grd.Name [RawMaterialgroup],                   
p.GrossInputWeight GrossInputWeight,                  
p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent,                             
cast(0 as decimal(18,5)) SetteledUnitRate,                   
cast(0 as decimal(18,5)) RevisedUnitRate,                 
sum( isnull(nullif(br.swRatio,0),100) * 0.01 * isnull(br.SetteledUnitRate,0) * isnull(nullif(br.slRatio,0),1)) MixtureUnitRateS,                
sum( isnull(nullif(br.rwRatio,0),100) * 0.01 * isnull(br.RevisedUnitRate,0) * isnull(nullif(br.rlRatio,0),1)) MixtureUnitRateR,    
sum(isnull(br.SetteledScrapPer,0))SetteledScrapPer,                                 
sum(isnull(br.RevisedScrapPer,0))RevisedScrapPer, gd.CurrentExwPrice, max(isnull(gd.PackagingCost,0)) PackagingCost, max(isnull(gd.LogisticsCost,0)) LogisticsCost, gd.PlantCode,                 
max(isnull(cast(gd.SOB as float),0)) SOB, gd.EPU, p.BuyerId, p.SupplierId, 0 IsParent,grd.Name RMGrade,                      
max(isnull(p.ConversionCost,0)) ConversionCost, max(isnull(p.RMReferenceCost,0)) RMReferenceCost, 0 [CurrentCostPer], 0 [RevisedCostPer], p.RMReference                  
, max(br.settledDate),max(br.RevisedDate)                   
,pp.PartNo ParentPartNo,1 IsParentSubMixture,1 IsSubPart,0 IsSubMixture,0.00,0 isparentmixture                
                        
from SubParts p                   
                  
inner join parts pp on p.ParentPartNo=pp.PartNo and p.SupplierId = pp.SupplierId and p.BuyerId = pp.BuyerId                            
left outer join RawMaterialMixtures m on m.SupplierId = p.SupplierId and m.BuyerId = p.BuyerId and m.RMGroupId = p.RMGradeId                            
left outer join RawMaterialGrades g on g.id = m.RawMaterialGradeId                  
left outer join RawMaterialGrades grd on grd.Id = p.RMGradeId 
left outer join RawMaterialGrades grp on grp.Id = p.RMGroupId
left outer join vw_RMTrend br on br.Id = (select top 1 (b.Id) from vw_rmtrend b where br.rmgroupid = b.rmgroupid and b.BuyerId = br.BuyerId and b.SupplierId = br.SupplierId                     
    and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)) order by b.reviseddate desc)                   
    and g.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId                  
left outer join GlobusData gd on gd.PartNo = p.ParentPartNo and gd.id =                  
 (select top 1 d.Id from GlobusData d where d.PartNo = gd.PartNo and d.Status in ('Auto Confirmed', 'Released', 'Confirmed')                   
 and (d.FromDate < @startdate or d.FromDate < @enddate) --and (d.ToDate >= @startdate or d.ToDate >= @enddate)                  
 and d.BuyerId = gd.BuyerId and gd.SupplierId = d.SupplierId and gd.PlantCode = d.plantcode and isnull(gd.ES1,'') = isnull(d.ES1, '') and isnull(gd.ES2, '') = isnull(d.ES2, '')                             
      order by  FromDate desc, id desc) and  p.BuyerId = gd.BuyerId and gd.SupplierId = p.SupplierId                  
                      
      and gd.plantcode in (SELECT code FROM Plants WHERE @plant IS NULL UNION ALL SELECT code FROM #PlantTable WHERE @plant IS NOT NULL)                
                  
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId  and isnull(grd.HasMixture, 0) = 1                  
 --and case when isnull(@GroupId, 0) = 0 then 1 else @GroupId end = case when isnull(@GroupId, 0) = 0 then 1 else grd.Id end 
 and case when isnull(@GradeId, 0) = 0 then 1 else @GradeId end = case when isnull(@GradeId, 0) = 0 then 1 else grp.Id end  
 and case when isnull(@SpecId, 0) = 0 then 1 else @SpecId end = case when isnull(@SpecId, 0) = 0 then 1 else grd.Id end 
 and gd.plantcode in (SELECT code FROM Plants WHERE @plant IS NULL UNION ALL SELECT code FROM #PlantTable WHERE @plant IS NOT NULL)                
group by P.Id, p.PartNo, p.Description, gd.ES1, gd.ES2, grd.Name, p.GrossInputWeight, p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent,                    
  gd.CurrentExwPrice, gd.PackagingCost, gd.LogisticsCost, gd.PlantCode, gd.SOB, gd.EPU, p.BuyerId, p.SupplierId, pp.IsParent, p.RMReference,pp.PartNo                
--,br.settleddate,br.reviseddate                             
order by p.Id                  
                
   update t set t.RevisedUnitRate = MixtureUnitRateR + ConversionCost, t.SetteledUnitRate = t.MixtureUnitRateS + ConversionCost,                 
   t.CurrentCostPer = t.MixtureUnitRateS + ConversionCost, t.RevisedCostPer = MixtureUnitRateR + ConversionCost from #hasSubPart t where isparentSubMixture = 1                  
                
   update t set t.RevisedUnitRate = RevisedUnitRate + ConversionCost, t.SetteledUnitRate = t.SetteledUnitRate + ConversionCost,                 
   t.CurrentCostPer = t.SetteledUnitRate + ConversionCost, t.RevisedCostPer = RevisedUnitRate + ConversionCost from #hasSubPart t where isparentSubMixture = 0                
                  
update #hasSubPart set [ScrapRecovery] = isnull([CastingForgingWeight],0) - isnull([FinishedWeight],0)                           
update #hasSubPart set [ScrapWeight] = isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)                      
update #hasSubPart set [CurrentRMCost] = Round((isnull([CastingForgingWeight],0) * isnull([SetteledUnitRate],0)) - (isnull([SetteledScrapPer],0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2),                   
        [RevisedRMCost] = Round((isnull([RevisedUnitRate],0) * isnull([CastingForgingWeight],0)) - (isnull([RevisedScrapPer],0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2)                  
update #hasSubPart set  [OtherCost] = isnull([CurrentExwPrice],0) - isnull([CurrentRMCost],0)                     
update #hasSubPart set Delta = (isnull(RevisedRMCost,0) - isnull(CurrentRMCost,0))                        
                
update hm set CurrentExwPrice = hm.CurrentExwPrice  + isnull((select (sum(isnull(hhm.Delta,0))) from #hasSubPart hhm where hm.ParentPartNo=hhm.ParentPartNo                 
--and isnull(hhm.settledDate,hhm.RevisedDate) < isnull(hm.settledDate,hm.RevisedDate)                             
and hhm.id<hm.id and isnull(hm.ES1,'') = isnull(hhm.ES1,'') and isnull(hm.ES2, '') = isnull(hhm.ES2,'')                            
and hhm.PlantCode=hm.PlantCode),0)                            
from #hasSubPart hm                          
                
update hm set RevisedExwPrice = hm.CurrentExwPrice + isnull(hm.Delta,0) from #hasSubPart hm ;                  
----------------------------------------------------------------------*************---------------------------------------------------------------------                  
 WITH CTE AS (      
  SELECT p.id tid, p.PartNo tpart, s.id sid, s.PartNo spart,p.PlantCode,s.ParentPartNo,      
         ROW_NUMBER() OVER (PARTITION BY p.id ORDER BY s.IsParentSubMixture) AS RowNum      
  FROM #temp p      
  JOIN #hasSubPart s ON p.PartNo = s.ParentPartNo and p.PlantCode=s.PlantCode      
)      
SELECT distinct sid,PlantCode,tid, tpart, CONCAT(tid, '.', RowNum)  AS SubpartID, spart,ParentPartNo      
into #hassubpartsid      
--drop table #hassubpartsid      
      
FROM CTE      
ORDER BY SubpartID ;      
 update s set s.slno= cast(i.SubpartID as nvarchar(255))  from #hasSubPart s inner join #hassubpartsid i on s.ParentPartNo=i.ParentPartNo and s.plantcode=i.plantcode  and s.id=i.sid;     
 ----------------------------------------------------------------------*************---------------------------------------------------------------------    
-------------------------------                    
--SumsubParts                  
select t.PartNo, t.PlantCode, sum(s.[CastingForgingWeight]) [CastingForgingWeight], sum(s.[FinishedWeight]) [FinishedWeight], sum(s.[GrossInputWeight]) [GrossInputWeight], AVG(s.[ScrapRecoveryPercent]) [ScrapRecoveryPercent],                  
sum(s.SetteledUnitRate) SetteledUnitRate, sum(s.RevisedUnitRate) RevisedUnitRate, sum(s.MixtureUnitRateS) MixtureUnitRateS, sum(s.MixtureUnitRateR) MixtureUnitRateR, sum(s.SetteledScrapPer) ScrapRateSetteled, sum(s.RevisedScrapPer) ScrapRateRevised,     
   
    
     
         
         
            
      --[CurrentRMCost] = sum((isnull(s.[GrossInputWeight],0) * isnull(s.[SetteledUnitRate],0)) - (isnull(s.SetteledScrapPer,0) * isnull(s.[CastingForgingWeight] - s.[FinishedWeight],0) * isnull(s.[ScrapRecoveryPercent],0))),                      
   --[RevisedRMCost] = sum((isnull(s.RevisedUnitRate,0) * isnull(s.[GrossInputWeight],0)) - (isnull(s.RevisedScrapPer,0) * isnull(s.[CastingForgingWeight] - s.[FinishedWeight],0) * isnull(s.[ScrapRecoveryPercent],0))),                           
               [CurrentRMCost] = sum(s.[CurrentRMCost]),                
               [RevisedRMCost] = sum(s.[RevisedRMCost]),                
   max(s.settledDate) settledDate,max(s.RevisedDate) RevisedDate,                     
   [CurrentCostPer] = sum(cast(s.CurrentCostPer as decimal(18,5))), [RevisedCostPer] = sum(cast(s.RevisedCostPer  as decimal(18,5)))                
into #subparttotal                   
from #hasSubPart s                     
     inner join #temp t on s.ParentPartNo = t.PartNo and s.BuyerId = t.BuyerId and s.SupplierId = t.SupplierId and t.PlantCode=s.PlantCode and isnull(s.ES1,'') = isnull(t.ES1, '') and isnull(s.es2, '') = isnull(t.es2,'')  group by t.PartNo, t.PlantCode, 
   
    
      
        
          
            
              
                
t.ES1, t.es2                    
insert into #temp (slno,[PartNo], [Description], [ES1], [ES2], [RawMaterialgroup],[RawMaterialGrade], [GrossInputWeight], [CastingForgingWeight], [FinishedWeight], [ScrapRecoveryPercent],           
[SetteledUnitRate], [RevisedUnitRate], MixtureUnitRateS, MixtureUnitRateR, [ScrapRateSetteled],                             
                 
[ScrapRateRevised], [CurrentExwPrice],[RevisedExwPrice], [PackagingCost], [LogisticsCost], [PlantCode], [SOB], [GlobusEPU], [BuyerId], [SupplierId], [IsParent], RMGrade, [ConversionCost], [RMRerfPrice], [CurrentCostPer],                       
      [RevisedCostPer], [RMReference], [settledDate], [RevisedDate],[Delta],[IsSubMixture], ParentPartNo,IsSubPart,RMgrdId,isparentSubMixture,OtherCost,CurrentRMCost,RevisedRMCost,isparentMixture)                        
      select slNo,PartNo, Description, ES1, ES2, Rawmaterialgroup,rawmaterialGrade, GrossInputWeight, CastingForgingWeight, FinishedWeight, ScrapRecoveryPercent, SetteledUnitRate, RevisedUnitRate, MixtureUnitRateS, MixtureUnitRateR, SetteledScrapPer,     
  
    
           
        
          
            
              
               
      RevisedScrapPer, CurrentExwPrice,RevisedExwPrice, PackagingCost, LogisticsCost, PlantCode, SOB, EPU, BuyerId, SupplierId, IsParent, RMGrade, ConversionCost, RMReferenceCost, CurrentCostPer,                    
      RevisedCostPer, RMReference, settledDate, RevisedDate,Delta,IsSubmixture, ParentPartNo,IsSubPart,RMgrdId,isparentSubMixture,OtherCost,CurrentRMCost,RevisedRMCost,isparentmixture from #hasSubPart order by ParentPartNo, isparentSubMixture, id --PartNo
  
    
      
        
          
            
              
, ES1, ES2                            
                   
update t set t.[CastingForgingWeight] = s.[CastingForgingWeight], t.[FinishedWeight] = s.[FinishedWeight],                    
 t.[GrossInputWeight] = s.[GrossInputWeight], t.[ScrapRecoveryPercent] = s.[ScrapRecoveryPercent],                     
 t.SetteledUnitRate = s.SetteledUnitRate, t.RevisedUnitRate = s.RevisedUnitRate, t.MixtureUnitRateS = s.MixtureUnitRateS, t.MixtureUnitRateR = s.MixtureUnitRateR, t.ScrapRateSetteled = s.ScrapRateSetteled,                    
 t.ScrapRateRevised = s.ScrapRateRevised, t.CurrentRMCost  = s.CurrentRMCost, t.RevisedRMCost = s.RevisedRMCost ,t.settledDate=s.settledDate,t.RevisedDate=s.RevisedDate,                
t.CurrentCostPer = s.CurrentCostPer, t.RevisedCostPer = s.RevisedCostPer                
from #temp t inner join #subparttotal s on t.PartNo = s.PartNo and t.PlantCode = s.Plantcode and t.IsParent = 1                  
---------------------------------                  
                  
                  
 -----------------------d----------------------                  
 --subHasMixture                  
                  
select slno,m.id, p.PartNo, p.Description, gd.ES1, gd.ES2,grde.Name rawmaterialgroup, g.Name, (p.GrossInputWeight*br.rwRatio * 0.01) GrossInputWeight,br.rwRatio WtRatio,  P.GrossInputWeight refGrossInputWeight,                         
p.CastingForgingWeight refCastingForgingWeight,  (p.CastingForgingWeight*br.rwRatio * 0.01) CastingForgingWeight  , p.FinishedWeight  refFinishedWeight, (p.FinishedWeight*br.rwRatio * 0.01) FinishedWeight, p.ScrapRecoveryPercent,                          
  
 cast(0.00 as decimal(18,5)) SetteledUnitRate,                        
cast(0.00 as decimal(18,5)) RevisedUnitRate,                        
isnull(br.SetteledUnitRate,0) MixtureUnitRateS,                
isnull(br.RevisedUnitRate,0) MixtureUnitRateR,                
                
(isnull(br.SetteledScrapPer,0)) SetteledScrapPer,                   
(isnull(br.RevisedScrapPer,0)) RevisedScrapPer, spp.CurrentExwPrice, (isnull(gd.PackagingCost,0))PackagingCost, (isnull(gd.LogisticsCost,0)) LogisticsCost, gd.PlantCode, (isnull(cast(gd.SOB as float),0)) SOB,                   
                        
                         
gd.EPU, p.BuyerId, p.SupplierId, 0 IsParent,                           
g.Name RMGrade,                       
(isnull(p.ConversionCost,0)) ConversionCost, (isnull(p.RMReferenceCost,0)) RMReferenceCost, 0  [CurrentCostPer], 0 [RevisedCostPer], p.RMReference ,br.settledDate,br.RevisedDate ,                      
cast(0.00 as decimal(18,5)) [ScrapRecovery], cast(0.00 as decimal(18,5)) [ScrapWeight] , cast(0.00 as decimal(18,5)) [CurrentRMCost], cast(0.00 as decimal(18,5))   [RevisedRMCost],                     
cast(0.00 as decimal(18,5)) [Delta], cast(0.00 as decimal(18,5)) [RevisedExwPrice] ,1 [Issubmixture], p.ParentPartNo [ParentPartNo],1 IsSubPart,0 IsParentSubMixture,0 isparentmixture                          
 into #SubhasMixture                      
from SubParts p                  
--inner join #hasSubPart spp on spp.PartNo=p.PartNo and =spp.PlantCode                  
left outer join RawMaterialMixtures m on m.SupplierId = p.SupplierId and m.BuyerId = p.BuyerId and m.RMGroupId = p.RMGradeId                  
left outer join RawMaterialGrades g on g.id = m.RawMaterialGradeId                  
left outer join RawMaterialGrades grd on grd.Id = p.RMGradeId                 
left outer join RawMaterialGrades grde on grde.Id = g.RawMaterialGradeId                
left outer join vw_RMTrend br on br.Id = (select top 1 (b.Id) from vw_rmtrend b where br.rmgroupid = b.rmgroupid and b.BuyerId = br.BuyerId and b.SupplierId = br.SupplierId                             
    and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)) order by b.reviseddate desc)                   
    and g.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId                  
left outer join GlobusData gd on gd.PartNo = p.ParentPartNo and gd.id =                   
       (select top 1 d.Id from GlobusData d where d.PartNo = gd.PartNo and d.Status in ('Auto Confirmed', 'Released', 'Confirmed')                   
     and (d.FromDate < @startdate or d.FromDate < @enddate)                 
     and d.BuyerId = gd.BuyerId and gd.SupplierId = d.SupplierId and gd.PlantCode = d.plantcode and isnull(gd.ES1,'') = isnull(d.ES1, '') and isnull(gd.ES2, '') = isnull(d.ES2, '') order by  FromDate desc, id desc)                             
  and  p.BuyerId = gd.BuyerId and gd.SupplierId = p.SupplierId                             
     and gd.plantcode in (SELECT code  FROM Plants  WHERE @plant IS NULL  UNION ALL  SELECT code  FROM #PlantTable  WHERE @plant IS NOT NULL)                  
inner join #hasSubPart spp on spp.PartNo=p.PartNo and gd.PlantCode =spp.PlantCode AND spp.ParentPartNo = p.ParentPartNo                  
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId  and isnull(grd.HasMixture, 0) = 1                  
 --and case when isnull(@GroupId, 0) = 0 then 1 else @GroupId end = case when isnull(@GroupId, 0) = 0 then 1 else grd.Id end                          
 and gd.plantcode in (SELECT code  FROM Plants  WHERE @plant IS NULL  UNION ALL  SELECT code  FROM #PlantTable  WHERE @plant IS NOT NULL)                
order by p.Id                         
                    
update t set t.RevisedUnitRate = MixtureUnitRateR + ConversionCost, t.SetteledUnitRate = t.MixtureUnitRateS + ConversionCost,     
t.CurrentCostPer = t.MixtureUnitRateS + ConversionCost, t.RevisedCostPer = MixtureUnitRateR + ConversionCost from #SubhasMixture t                
                      
update #SubhasMixture set GrossInputWeight=(refCastingForgingWeight*WtRatio * 0.01)                    
                    
update #SubhasMixture set CastingForgingWeight=(GrossInputWeight/refGrossInputWeight)*refCastingForgingWeight                    
                    
update #SubhasMixture set FinishedWeight=(CastingForgingWeight/refCastingForgingWeight)*refFinishedWeight                
                    
update #SubhasMixture set [ScrapRecovery] = isnull([CastingForgingWeight],0) - isnull([FinishedWeight],0)                      
update #SubhasMixture set [ScrapWeight] = isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)                    
update #SubhasMixture set [CurrentRMCost] = Round((isnull([GrossInputWeight],0) * isnull([SetteledUnitRate],0)) - (isnull(SetteledScrapPer,0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2),                    
        [RevisedRMCost] = Round((isnull([RevisedUnitRate],0) * isnull([GrossInputWeight],0)) - (isnull(RevisedScrapPer,0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2)                  
update #SubhasMixture set Delta = (isnull(RevisedRMCost,0) - isnull(CurrentRMCost,0))                 
update hm set CurrentExwPrice = hm.CurrentExwPrice  + isnull((select (sum(isnull(hhm.Delta,0))) from #SubhasMixture hhm where hhm.PartNo = hm.PartNo and hhm.ParentPartNo = hm.ParentPartNo                          
--and isnull(hhm.settledDate,hhm.RevisedDate) < isnull(hm.settledDate,hm.RevisedDate)                 
and hhm.id < hm.id  and isnull(hm.ES1,'') = isnull(hhm.ES1,'') and isnull(hm.ES2, '') = isnull(hhm.ES2,'')                
and hhm.PlantCode=hm.PlantCode),0) from #SubhasMixture hm                     
update hm set RevisedExwPrice = hm.CurrentExwPrice + isnull(hm.Delta,0) from #SubhasMixture hm ;                   
    
----------------------------------------------------------------------*************---------------------------------------------------------------------      
WITH CTE AS (      
  SELECT p.slNo tid, p.PartNo tpart, s.id sid, s.PartNo spart,p.PlantCode,s.ParentPartNo,      
         ROW_NUMBER() OVER (PARTITION BY p.slno ORDER BY s.IsParentSubMixture) AS RowNum      
  FROM #hasSubPart p      
  JOIN #SubhasMixture s ON p.PartNo = s.PartNo and p.PlantCode=s.PlantCode and p.ParentPartNo=s.ParentPartNo      
)      
SELECT distinct sid,PlantCode,tid, tpart, CONCAT(tid, '.', RowNum) AS SubpartID, spart,ParentPartNo      
into #hassubmixtureid      
--drop table #hasid      
FROM CTE      
ORDER BY SubpartID ;      
      
update s set s.slno= cast(i.SubpartID as nvarchar(255))  from #SubhasMixture s inner join #hassubmixtureid i on s.ParentPartNo=i.ParentPartNo and s.plantcode=i.plantcode and s.id=i.sid      
----------------------------------------------------------------------*************---------------------------------------------------------------------                
insert into #temp (slno,[PartNo], [Description], [ES1], [ES2], [RawMaterialgroup],[RawMaterialGrade], [GrossInputWeight], [CastingForgingWeight], [FinishedWeight], [ScrapRecoveryPercent],           
[SetteledUnitRate], [RevisedUnitRate], [MixtureUnitRateS], [MixtureUnitRateR], [ScrapRateSetteled],                            
[ScrapRateRevised], [CurrentExwPrice], [PackagingCost], [LogisticsCost], [PlantCode], [SOB], [GlobusEPU], [BuyerId], [SupplierId], [IsParent], RMGrade, [ConversionCost], [RMRerfPrice], [CurrentCostPer],                            
      [RevisedCostPer], [RMReference], [settledDate], [RevisedDate],[Delta],[IsSubMixture],ParentPartNo,IsSubPart,IsParentSubMixture,isparentMixture)                        
      select slno,PartNo, Description, ES1, ES2,rawmaterialgroup, Name, GrossInputWeight, CastingForgingWeight, FinishedWeight, ScrapRecoveryPercent, SetteledUnitRate, RevisedUnitRate, MixtureUnitRateS, MixtureUnitRateS, SetteledScrapPer,                 
 
     
        
      RevisedScrapPer, CurrentExwPrice, PackagingCost, LogisticsCost, PlantCode, SOB, EPU, BuyerId, SupplierId, IsParent, RMGrade, ConversionCost, RMReferenceCost, CurrentCostPer,                    
      RevisedCostPer, RMReference, settledDate, RevisedDate,Delta,IsSubmixture,ParentPartNo,IsSubPart,IsParentSubMixture,isparentmixture from #SubhasMixture                  
 ----------------------------------------------------------------------                  
                  
                  
 --sumMixturePart                    
insert #temp (slno,[PartNo], [Description], [ES1], [ES2], [RawMaterialGrade], [GrossInputWeight], [CastingForgingWeight], [FinishedWeight], [ScrapRecoveryPercent],                 
[SetteledUnitRate], [RevisedUnitRate], [MixtureUnitRateS], [MixtureUnitRateR], [ScrapRateSetteled],                         
[ScrapRateRevised], [CurrentExwPrice], [PackagingCost], [LogisticsCost], [PlantCode], [SOB], [GlobusEPU], [BuyerId], [SupplierId], [IsParent], RMGrade, [ConversionCost], [RMRerfPrice], [CurrentCostPer], [RevisedCostPer], [RMReference]                     
  
,[settledDate],[RevisedDate]                     
,ParentPartNo,RMgrdId,IsParentSubMixture, IsSubPart,isparentMixture,IsSubMixture)                   
                          
select cast(((ROW_NUMBER() OVER (ORDER BY p.id))+(isnull(@hasid,0))) as nvarchar(255))slNo,p.PartNo, p.Description, gd.ES1, gd.ES2,                  
'',                  
--grd.Name,                            
p.GrossInputWeight GrossInputWeight,                    
--p.CastingForgingWeight GrossInputWeight,                    
p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent,                 
cast(0 as decimal(18,2)) SetteledUnitRate,                
cast(0 as decimal(18,2)) RevisedUnitRate,                
sum( isnull(nullif(br.swRatio,0),100) * 0.01 * isnull(br.SetteledUnitRate,0) * isnull(nullif(br.slRatio,0),1)) MixtureUnitRateS,                 
sum( isnull(nullif(br.rwRatio,0),100) * 0.01 * isnull(br.RevisedUnitRate,0) * isnull(nullif(br.rlRatio,0),1))  MixtureUnitRateR, sum(isnull(br.SetteledScrapPer,0)),                     
sum(isnull(br.RevisedScrapPer,0)), gd.CurrentExwPrice, max(isnull(gd.PackagingCost,0)), max(isnull(gd.LogisticsCost,0)), gd.PlantCode, max(isnull(cast(gd.SOB as float),0)), gd.EPU, p.BuyerId, p.SupplierId,                 
case when isnull(p.IsParent, 0) = 0 then 0 else 1 end,grd.Name RMGrade,                 
max(isnull(p.ConversionCost,0)), max(isnull(p.RMReferenceCost,0)), 0, 0, p.RMReference                            
, max(br.settledDate),max(br.RevisedDate)                     
,p.PartNo,p.RMGradeId,0,0,1,0                   
from parts p                     
left outer join RawMaterialMixtures m on m.SupplierId = p.SupplierId and m.BuyerId = p.BuyerId and m.RMGroupId = p.RMGradeId                    
left outer join RawMaterialGrades g on g.id = m.RawMaterialGradeId                    
left outer join RawMaterialGrades grd on grd.Id = p.RMGradeId                  
left outer join RawMaterialGrades grp on grp.Id = p.RMGroupId                    
left outer join vw_RMTrend br on br.Id = (select top 1 (b.Id) from vw_rmtrend b where br.rmgroupid = b.rmgroupid and b.BuyerId = br.BuyerId and b.SupplierId = br.SupplierId                   
    and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)) order by b.reviseddate desc)                     
    and g.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId                    
left outer join GlobusData gd on gd.PartNo = p.PartNo and gd.id =                    
 (select top 1 d.Id from GlobusData d where d.PartNo = gd.PartNo and d.Status in ('Auto Confirmed', 'Released', 'Confirmed')                     
 and (d.FromDate < @startdate or d.FromDate < @enddate) --and (d.ToDate >= @startdate or d.ToDate >= @enddate)                  
 and d.BuyerId = gd.BuyerId and gd.SupplierId = d.SupplierId and gd.PlantCode = d.plantcode and isnull(gd.ES1,'') = isnull(d.ES1, '') and isnull(gd.ES2, '') = isnull(d.ES2, '') order by  FromDate desc, id desc)                   
                  
                        
      and  p.BuyerId = gd.BuyerId and gd.SupplierId = p.SupplierId                
                      
      and gd.plantcode in (SELECT code                           
      FROM Plants                
      WHERE @plant IS NULL                
      UNION ALL                    
      SELECT code                
      FROM #PlantTable                
      WHERE @plant IS NOT NULL)                         
                    
                  
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId                    
and isnull(grd.HasMixture, 0) = 1                    
 --and case when isnull(@GroupId, 0) = 0 then 1 else @GroupId end = case when isnull(@GroupId, 0) = 0 then 1 else grd.Id end 
 and case when isnull(@GradeId, 0) = 0 then 1 else @GradeId end = case when isnull(@GradeId, 0) = 0 then 1 else grp.Id end 
 and case when isnull(@SpecId, 0) = 0 then 1 else @SpecId end = case when isnull(@SpecId, 0) = 0 then 1 else grd.Id end
                      
    and gd.plantcode in (SELECT code                         
        FROM Plants                          
        WHERE @plant IS NULL                
        UNION ALL                
        SELECT code                
        FROM #PlantTable                
        WHERE @plant IS NOT NULL)                         
                      
group by P.Id, p.PartNo, p.Description, gd.ES1, gd.ES2, grd.Name, p.GrossInputWeight, p.CastingForgingWeight, p.FinishedWeight, p.ScrapRecoveryPercent,                      
  gd.CurrentExwPrice, gd.PackagingCost, gd.LogisticsCost, gd.PlantCode, gd.SOB, gd.EPU, p.BuyerId, p.SupplierId, p.IsParent, p.RMReference,p.RMGradeId                    
 --,br.settleddate,br.reviseddate                   
order by p.Id                
---------------------------------------------------------------------------------------------------------------------------------------------------                  
--hasMixture                        
                  
select slno,m.id,p.PartNo, p.Description, gd.ES1, gd.ES2,grde.Name rawmaterialgroup, g.Name, (p.GrossInputWeight*br.rwRatio * 0.01) GrossInputWeight,br.rwRatio WtRatio,  P.GrossInputWeight refGrossInputWeight,                     
p.CastingForgingWeight refCastingForgingWeight,  (p.CastingForgingWeight*br.rwRatio * 0.01) CastingForgingWeight  , p.FinishedWeight  refFinishedWeight,                     
(p.FinishedWeight*br.rwRatio * 0.01) FinishedWeight, p.ScrapRecoveryPercent,                           
cast(0.00 as decimal(18,5)) SetteledUnitRate,                 
cast(0.00 as decimal(18,5)) RevisedUnitRate,                
isnull(br.SetteledUnitRate,0) MixtureUnitRateS,                  
isnull(br.RevisedUnitRate,0) MixtureUnitRateR,                     
(isnull(br.SetteledScrapPer,0)) SetteledScrapPer,                       
(isnull(br.RevisedScrapPer,0)) RevisedScrapPer, gd.CurrentExwPrice, (isnull(gd.PackagingCost,0))PackagingCost, (isnull(gd.LogisticsCost,0)) LogisticsCost, gd.PlantCode,                     
(isnull(cast(gd.SOB as float),0)) SOB,                   
gd.EPU, p.BuyerId, p.SupplierId, 0 IsParent,       
g.Name RMGrade,                             
(isnull(p.ConversionCost,0)) ConversionCost, (isnull(p.RMReferenceCost,0)) RMReferenceCost, 0  [CurrentCostPer], 0 [RevisedCostPer], p.RMReference ,br.settledDate,br.RevisedDate ,                          
cast(0.00 as decimal(18,5)) [ScrapRecovery], cast(0.00 as decimal(18,5)) [ScrapWeight] , cast(0.00 as decimal(18,5)) [CurrentRMCost], cast(0.00 as decimal(18,5))   [RevisedRMCost],                 
cast(0.00 as decimal(18,5)) [Delta], cast(0.00 as decimal(18,5)) [RevisedExwPrice] ,1 [Issubmixture], p.PartNo [ParentPartNo],0 IsParentSubMixture,0 IsSubPart,0 isparentmixture                      
 into #hasMixture                      
from Parts p          
left outer join #temp tp on tp.BuyerId=p.BuyerId and tp.SupplierId=p.SupplierId and tp.id= (select id from #temp  where tp.isparentmixture=0 and  tp.[IsSubMixture]=1 and tp.[IsSubPart]=0 and [isparentSubMixture]=0)      
left outer join RawMaterialMixtures m on m.SupplierId = p.SupplierId and m.BuyerId = p.BuyerId and m.RMGroupId = p.RMGradeId                 
left outer join RawMaterialGrades g on g.id = m.RawMaterialGradeId                      
left outer join RawMaterialGrades grd on grd.Id = p.RMGradeId                  
left outer join RawMaterialGrades grde on grde.Id = g.RawMaterialGradeId                  
left outer join vw_RMTrend br on br.Id = (select top 1 (b.Id) from vw_rmtrend b where br.rmgroupid = b.rmgroupid and b.BuyerId = br.BuyerId and b.SupplierId = br.SupplierId                             
    and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)) order by b.reviseddate desc)                  
    and g.id = br.rmgroupid and p.BuyerId = br.buyerid and p.SupplierId = br.SupplierId                         
left outer join GlobusData gd on gd.PartNo = p.PartNo and gd.id =                       
 (select top 1 d.Id from GlobusData d where d.PartNo = gd.PartNo and d.Status in ('Auto Confirmed', 'Released', 'Confirmed')                       
     and (d.FromDate < @startdate or d.FromDate < @enddate)                         
     and d.BuyerId = gd.BuyerId and gd.SupplierId = d.SupplierId and gd.PlantCode = d.plantcode and isnull(gd.ES1,'') = isnull(d.ES1, '') and isnull(gd.ES2, '') = isnull(d.ES2, '') order by FromDate desc, id desc)                
  and  p.BuyerId = gd.BuyerId and gd.SupplierId = p.SupplierId                   
     and gd.plantcode in (SELECT code  FROM Plants  WHERE @plant IS NULL  UNION ALL  SELECT code  FROM #PlantTable  WHERE @plant IS NOT NULL)                   
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId  and isnull(grd.HasMixture, 0) = 1                      
 --and case when isnull(@GroupId, 0) = 0 then 1 else @GroupId end = case when isnull(@GroupId, 0) = 0 then 1 else grd.Id end                      
 and gd.plantcode in (SELECT code  FROM Plants  WHERE @plant IS NULL  UNION ALL  SELECT code  FROM #PlantTable  WHERE @plant IS NOT NULL)                       
order by p.Id ;      
      
      
----------------------------------------------------------------------*************---------------------------------------------------------------------      
WITH CTE AS (      
  SELECT p.id tid, p.PartNo tpart, s.id sid, s.PartNo spart,p.PlantCode,s.ParentPartNo,      
         ROW_NUMBER() OVER (PARTITION BY p.id ORDER BY s.IsParentSubMixture) AS RowNum      
  FROM #temp p      
  JOIN #hasMixture s ON p.PartNo = s.ParentPartNo and p.PlantCode=s.PlantCode      
)      
SELECT distinct sid,PlantCode,tid, tpart, CONCAT(tid, '.', RowNum)  AS SubpartID, spart,ParentPartNo      
into #hasmixtureid      
FROM CTE      
ORDER BY SubpartID ;      
    
update s set s.slno= cast(i.SubpartID as nvarchar(255))  from #hasMixture s inner join #hasmixtureid i on s.ParentPartNo=i.ParentPartNo and s.plantcode=i.plantcode and s.id=i.sid      
----------------------------------------------------------------------*************---------------------------------------------------------------------      
      
      
      
      
                
update t set t.RevisedUnitRate = MixtureUnitRateR + ConversionCost, t.SetteledUnitRate = t.MixtureUnitRateS + ConversionCost, t.CurrentCostPer = t.MixtureUnitRateS + ConversionCost, t.RevisedCostPer = MixtureUnitRateR + ConversionCost from #hasMixture t 
   
   
       
                            
update #hasMixture set GrossInputWeight=(refCastingForgingWeight*WtRatio * 0.01)                        
                        
update #hasMixture set CastingForgingWeight=(GrossInputWeight/refGrossInputWeight)*refCastingForgingWeight                        
                        
update #hasMixture set FinishedWeight=(CastingForgingWeight/refCastingForgingWeight)*refFinishedWeight                        
                        
update #hasMixture set [ScrapRecovery] = isnull([CastingForgingWeight],0) - isnull([FinishedWeight],0)                          
update #hasMixture set [ScrapWeight] = isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)            
update #hasMixture set [CurrentRMCost] = Round((isnull([GrossInputWeight],0) * isnull([SetteledUnitRate],0)) - (isnull(SetteledScrapPer,0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2),                      
        [RevisedRMCost] = Round((isnull([RevisedUnitRate],0) * isnull([GrossInputWeight],0)) - (isnull(RevisedScrapPer,0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2)                        
                        
update #hasMixture set Delta = (isnull(RevisedRMCost,0) - isnull(CurrentRMCost,0))                        
                        
update hm set CurrentExwPrice = hm.CurrentExwPrice  + isnull((select (sum(isnull(hhm.Delta,0))) from #hasMixture hhm where hhm.PartNo = hm.PartNo                   
--and isnull(hhm.settledDate,hhm.RevisedDate) < isnull(hm.settledDate,hm.RevisedDate)                
and hhm.id < hm.id  and isnull(hm.ES1,'') = isnull(hhm.ES1,'') and isnull(hm.ES2, '') = isnull(hhm.ES2,'')                             
 and hhm.PlantCode=hm.PlantCode),0) from #hasMixture hm                         
                        
update hm set RevisedExwPrice = hm.CurrentExwPrice + isnull(hm.Delta,0) from #hasMixture hm                        
                        
insert into #temp (slno,[PartNo], [Description], [ES1], [ES2], [RawMaterialgroup],[RawMaterialGrade], [GrossInputWeight], [CastingForgingWeight], [FinishedWeight], [ScrapRecoveryPercent],           
[SetteledUnitRate], [RevisedUnitRate], MixtureUnitRateS, MixtureUnitRateR, [ScrapRateSetteled],                             
 [ScrapRateRevised], [CurrentExwPrice], [PackagingCost], [LogisticsCost], [PlantCode], [SOB], [GlobusEPU], [BuyerId], [SupplierId], [IsParent], RMGrade, [ConversionCost], [RMRerfPrice], [CurrentCostPer],                          
      [RevisedCostPer], [RMReference], [settledDate], [RevisedDate],[Delta],[IsSubMixture],ParentPartNo, IsParentSubMixture, IsSubPart,isparentMixture)                        
      select slno,PartNo, Description, ES1, ES2,rawmaterialgroup, Name, GrossInputWeight, CastingForgingWeight, FinishedWeight, ScrapRecoveryPercent, SetteledUnitRate, RevisedUnitRate, MixtureUnitRateS, MixtureUnitRateR, SetteledScrapPer,                
 
      RevisedScrapPer, CurrentExwPrice, PackagingCost, LogisticsCost, PlantCode, SOB, EPU, BuyerId, SupplierId, IsParent, RMGrade, ConversionCost, RMReferenceCost, CurrentCostPer,                        
      RevisedCostPer, RMReference, settledDate, RevisedDate,Delta,IsSubmixture,ParentPartNo,IsParentSubMixture, IsSubPart,isparentmixture from #hasMixture            
                              
--update t set t.RevisedUnitRate = RevisedUnitRate + ConversionCost, t.SetteledUnitRate = t.SetteledUnitRate + ConversionCost, t.CurrentCostPer = t.SetteledUnitRate + ConversionCost, t.RevisedCostPer = RevisedUnitRate + ConversionCost from #temp t        
  
    
     
         
         
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------                  
                
 --FinalUpdateQueries                
                 
update #temp set [ScrapRecovery] = isnull([CastingForgingWeight],0) - isnull([FinishedWeight],0)  --  where isparentMixture = 1 or isparentSubMixture=1                  
                          
--update #temp set [ScrapRecovery] = isnull([GrossInputWeight],0) - isnull([FinishedWeight],0)  where IsParent = 0                   
                      
update #temp set [ScrapWeight] = isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)         
                  
update #temp set [CurrentRMCost] = Round((isnull([GrossInputWeight],0) * isnull([SetteledUnitRate],0)) - (isnull([ScrapRateSetteled],0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2),                          
     [RevisedRMCost] = Round((isnull([RevisedUnitRate],0) * isnull([GrossInputWeight],0)) - (isnull([ScrapRateRevised],0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2)  where IsParent = 0                     
---------------------------------------------                           
--Mixturecase                  
update #temp set [CurrentRMCost] = Round((isnull([CastingForgingWeight],0) * isnull([SetteledUnitRate],0)) - (isnull([ScrapRateSetteled],0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2),                      
     [RevisedRMCost] = Round((isnull([RevisedUnitRate],0) * isnull([CastingForgingWeight],0)) - (isnull([ScrapRateRevised],0) * isnull([ScrapRecovery],0) * isnull([ScrapRecoveryPercent],0)),2)  where isparentMixture = 1 or isparentSubMixture=1            
  
   
             
                
----------------------------------------------                  
                  
update #temp set [OtherCost] = isnull([CurrentExwPrice],0) - isnull([CurrentRMCost],0)                      
update #temp set [RevisedExwPrice] = ROUND(isnull([RevisedRMCost],0) + isnull([OtherCost],0),2)                       
update #temp set [ExwPriceChangeInCost] = [RevisedExwPrice] - isnull([CurrentExwPrice],0)                    
update #temp set [ExwPriceChangeInPer] = case when isnull([CurrentExwPrice],0) = 0 then 0 else [ExwPriceChangeInCost] / [CurrentExwPrice] * 100 end                    
update #temp set [CurrentFCAPrice] = isnull([CurrentExwPrice],0) + [PackagingCost] + [LogisticsCost], [RevisedFCAPrice] = [RevisedExwPrice] + [PackagingCost] + [LogisticsCost]                       
update #temp set [CurrentAVOB] = ([CurrentFCAPrice] * [GlobusEPU] * cast([SOB] as float) * .01) / power(10,6)                             
update #temp set [RevisedAVOB] = ([RevisedFCAPrice] * [GlobusEPU] * cast([SOB] as float) * .01) / power(10,6)                      
update #temp set [RMImpact] = ([GlobusEPU] * [ExwPriceChangeInCost] * cast([SOB] as float) * .01) / power(10,6)                         
                      
update #temp set [CurrentExwPrice] = isnull([CurrentExwPrice],0)                    
update #temp set [RawMaterialGrade] = isnull([RawMaterialGrade],'')                     
update #temp set [SetteledUnitRate] = isnull(SetteledUnitRate, 0)                    
update #temp set RevisedUnitRate = isnull(RevisedUnitRate, 0)                      
update #temp set ScrapRateSetteled = isnull(ScrapRateSetteled, 0)                    
update #temp set ScrapRateSetteled = isnull(ScrapRateSetteled, 0)                    
update #temp set CurrentAVOB = isnull(CurrentAVOB, 0)                    
update #temp set RevisedAVOB = isnull(RevisedAVOB, 0)                 
update #temp set SOB = isnull(SOB, 0)                    
update #temp set PlantCode = isnull(PlantCode, '')                    
update #temp set GlobusEPU = isnull(GlobusEPU, 0)                      
update #temp set RMImpact = isnull(RMImpact, 0)  , CurrentCostPer  = isnull(CurrentCostPer,0), RevisedCostPer = isnull(RevisedCostPer,0), [RMReference] = isnull([RMReference],''), ES1 = isnull(ES1, ''), ES2 = isnull(ES2, '')                  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                
--settledDateBuckets                
     
    
select rm.RMGrade, rm.RMSpec,rm.Buyer,rm.Supplier,BaseRMRate SBaseRMRates,RMSurchargeGradeDiff SRMSurchargeGradeDiff,SecondaryProcessing SSecondaryProcessing,SurfaceProtection SSurfaceProtection,Thickness SThickness,    
MOQVolume SMOQVolume,CuttingCost SCuttingCost,Transport STransport,Others SOthers,rm.SupplierId,rm.BuyerId,rm.date    
INTO #SettledBuckets    
from rmtaptool rm where rm.id in (    
select max(rm.id) from RMTapTool rm inner join  #temp t on rm.buyerid=t.BuyerId and rm.supplierid=t.SupplierId and rm.rmspec=t.RawMaterialGrade and rm.date<=t.settledDate  group by rmspec)    
    
    
    
-------------------------------------------------------------------------------------------------------------------------------------                          
--RevisedDateBuckets                          
    
    
select rm.RMGrade, rm.RMSpec,rm.Buyer,rm.Supplier,BaseRMRate RBaseRMRates,RMSurchargeGradeDiff RRMSurchargeGradeDiff,SecondaryProcessing RSecondaryProcessing,SurfaceProtection RSurfaceProtection,Thickness RThickness,    
MOQVolume RMOQVolume,CuttingCost RCuttingCost,Transport RTransport,Others ROthers,rm.BuyerId,rm.SupplierId,rm.date    
INTO #RevisedBuckets    
from rmtaptool rm where rm.id in (    
select max(rm.id) from RMTapTool rm inner join  #temp t on rm.buyerid=t.BuyerId and rm.supplierid=t.SupplierId and rm.rmspec=t.RawMaterialGrade and rm.date<=t.RevisedDate  group by rmspec)    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                
                
update #temp  set SRMSurchargeGradeDiff =  0, SSecondaryProcessing = 0, SSurfaceProtection = 0, SThickness = 0, SCuttingCost = 0, SMOQVolume = 0, STransport = 0, SOthers = 0,                
 RRMSurchargeGradeDiff =  0, RSecondaryProcessing = 0, RSurfaceProtection = 0, RThickness = 0, RCuttingCost = 0, RMOQVolume = 0, RTransport = 0, ROthers = 0                
                
update t set SRMSurchargeGradeDiff =  sb.SRMSurchargeGradeDiff, t.SSecondaryProcessing = sb.SSecondaryProcessing, t.SSurfaceProtection = sb.SSurfaceProtection, t.SThickness = sb.SThickness,                 
 t.SCuttingCost = sb.SCuttingCost, t.SMOQVolume = sb.SMOQVolume, t.STransport = sb.STransport, t.SOthers = sb.SOthers from #temp t inner join #SettledBuckets sb on t.RawMaterialGrade = sb.RMSpec                 
                
                
update t set RRMSurchargeGradeDiff =  sb.RRMSurchargeGradeDiff, t.RSecondaryProcessing = sb.RSecondaryProcessing, t.RSurfaceProtection = sb.RSurfaceProtection, t.RThickness = sb.RThickness,                 
 t.RCuttingCost = sb.RCuttingCost, t.RMOQVolume = sb.RMOQVolume, t.RTransport = sb.RTransport, t.ROthers = sb.ROthers from #temp t inner join #RevisedBuckets sb on t.RawMaterialGrade = sb.RMSpec                 
                
                
select t.ParentPartNo, t.PlantCode, t.ES1, t.ES2, SUM(t.SRMSurchargeGradeDiff) SRMSurchargeGradeDiff, sum(t.SSecondaryProcessing) SSecondaryProcessing, sum(t.SSurfaceProtection) SSurfaceProtection,       
sum(t.SThickness) SThickness, sum(t.SCuttingCost) SCuttingCost,                
 sum(t.SMOQVolume) SMOQVolume, sum(t.STransport) STransport, sum(t.SOthers) SOthers,                 
 SUM(t.RRMSurchargeGradeDiff) RRMSurchargeGradeDiff, sum(t.RSecondaryProcessing) RSecondaryProcessing, sum(t.RSurfaceProtection) RSurfaceProtection, sum(t.RThickness) RThickness, sum(t.RCuttingCost) RCuttingCost,                
 sum(t.RMOQVolume) RMOQVolume, sum(t.RTransport) RTransport, sum(t.ROthers) ROthers                 
into #buckettotal                
from #temp t                 
where isnull(t.IsSubPart,0) = 1 and isnull(t.IsParent,0) = 0 and isnull(t.isparentMixture,0)=0 and isnull(t.isparentSubMixture,0)=0 and isnull(t.IsSubMixture,0)=0                
group by t.ParentPartNo, t.PlantCode, t.ES1, t.ES2                
                
update tp set tp.SRMSurchargeGradeDiff = (t.SRMSurchargeGradeDiff), tp.SSecondaryProcessing = (t.SSecondaryProcessing), tp.SSurfaceProtection = (t.SSurfaceProtection),                 
 tp.SThickness = (t.SThickness), tp.SCuttingCost = (t.SCuttingCost), tp.SMOQVolume = (t.SMOQVolume), tp.STransport = (t.STransport), tp.SOthers = (t.SOthers),                 
 tp.RRMSurchargeGradeDiff = (t.RRMSurchargeGradeDiff), tp.RSecondaryProcessing = (t.RSecondaryProcessing), tp.RSurfaceProtection = (t.RSurfaceProtection), tp.RThickness = (t.RThickness),                 
 tp.RCuttingCost = (t.RCuttingCost), tp.RMOQVolume = (t.RMOQVolume), tp.RTransport = (t.RTransport), tp.ROthers = (t.ROthers)                 
from #buckettotal t inner join #temp tp on t.ParentPartNo = tp.PartNo and t.ParentPartNo = tp.ParentPartNo and t.PlantCode = tp.PlantCode and t.ES1 = tp.ES1 and t.ES2 = tp.ES2                 
where isnull(tp.IsParent,0) = 1 and isnull(tp.IsSubPart,0) = 0 and isnull(tp.isparentMixture,0)=0 and isnull(tp.isparentSubMixture,0)=0 and isnull(tp.IsSubMixture,0)=0                
                 
UPDATE #temp                
SET                
                
  SBaseRMRate = isnull((CurrentCostPer-(SRMSurchargeGradeDiff+SSecondaryProcessing+SSurfaceProtection+SThickness+SCuttingCost+SMOQVolume+STransport+SOthers)),0),                
  RBaseRMRate = isnull((RevisedCostPer-(RRMSurchargeGradeDiff+RSecondaryProcessing+RSurfaceProtection+RThickness+RCuttingCost+RMOQVolume+RTransport+ROthers)),0) ,                
  ProcessImpactTotal=([GlobusEPU] *  ((RRMSurchargeGradeDiff+RSecondaryProcessing+RSurfaceProtection+RThickness+RCuttingCost+RMOQVolume+RTransport+ROthers)-                
                                     (SRMSurchargeGradeDiff+SSecondaryProcessing+SSurfaceProtection+SThickness+SCuttingCost+SMOQVolume+STransport+SOthers)) * cast([SOB] as float) * .01) / power(10,6)                 
                 
 UPDATE #temp                
SET                
 RMImpactTotal=(RMImpact-ProcessImpactTotal)                
           
---*********************************************************************************************------------------------    
--update for serial number    
    
UPDATE #temp  
SET pslno = CASE  
        WHEN slno LIKE '%.%' THEN LEFT(slno, CHARINDEX('.', slno) - 1)  
        ELSE slno  
    END,      subslno = CASE  
        WHEN slno LIKE '%.%.%' THEN SUBSTRING(slno, CHARINDEX('.', slno) + 1, CHARINDEX('.', slno, CHARINDEX('.', slno) + 1) - CHARINDEX('.', slno) - 1)  
        WHEN slno LIKE '%.%' THEN SUBSTRING(slno, CHARINDEX('.', slno) + 1, LEN(slno))  
        ELSE '0'  
    END,  
    mixslno = CASE  
        WHEN slno LIKE '%.%.%' THEN RIGHT(slno, LEN(slno) - CHARINDEX('.', slno, CHARINDEX('.', slno) + 1))  
        WHEN slno LIKE '%.%' THEN '0'  
        ELSE '0'  
    END  
    
----*********************************************************************************************-------------------------    
                
if (@IsGenerateA3 = 1)                      
insert into A3PriceImpacts             
               (DocId,slno,PartNo,RawMaterialGroup,RawMaterialGrade,Description,GrossInputWeight,CastingForgingWeight,FinishedWeight,ScrapRecovery,ScrapRecoveryPercent,ScrapWeight,            
      CurrentRMCost,RevisedRMCost,OtherCost,CurrentExwPrice,RevisedExwPrice,ExwPriceChangeInCost,ExwPriceChangeInPer,PackagingCost,LogisticsCost,CurrentFCAPrice,RevisedFCAPrice,            
      CurrentAVOB,RevisedAVOB,PlantCode,SOB,GlobusEPU,RMImpact,IsParent,ConversionCost,CurrentCostPer,RevisedCostPer,RMReference,ES1,ES2,RMSurchargeGradeDiff,SecondaryProcessing,            
      SurfaceProtection,Thickness,CuttingCost,MOQVolume,Transport,Others,BaseRMRate,RevRMSurchargeGradeDiff,RevSecondaryProcessing,RevSurfaceProtection,RevThickness,RevCuttingCost,RevMOQVolume,          
   RevTransport,RevOthers,RevBaseRMRate,ProcessImpact,RMImpactt,           
           
      SubPart,IsParentMixture,IsParentSubMixture,SubMixture)            
                  
select @A3Id, 1, [PartNo],  RawMaterialgroup,[RMGrade], [Description], [GrossInputWeight], [CastingForgingWeight], [FinishedWeight], [ScrapRecovery], [ScrapRecoveryPercent], ScrapWeight,                   
              CurrentRMCost, RevisedRMCost, OtherCost, CurrentExwPrice, RevisedExwPrice, ExwPriceChangeInCost, ExwPriceChangeInPer, PackagingCost, LogisticsCost, CurrentFCAPrice, RevisedFCAPrice,                         
              CurrentAVOB, RevisedAVOB, PlantCode, SOB, GlobusEPU, RMImpact, IsParent, ConversionCost, CurrentCostPer, RevisedCostPer, [RMReference], [ES1], [ES2], SRMSurchargeGradeDiff, SSecondaryProcessing,                
              SSurfaceProtection, SThickness, SCuttingCost, SMOQVolume, STransport, SOthers,SBaseRMRate, RRMSurchargeGradeDiff, RSecondaryProcessing, RSurfaceProtection, RThickness, RCuttingCost, RMOQVolume,          
     RTransport,ROthers,RBaseRMRate,ProcessImpactTotal,RMImpactTotal,            
     IsSubPart,isparentMixture,isparentSubMixture,IsSubMixture               
              --from #temp order by PlantCode,ParentPartNo, ES1, es2, id, partno, isSubpart, IsSubMixture, IsParentSubMixture, settledDate     
     from #temp order by cast(pslno as int ),cast(subslno as int),cast(mixslNo as int),settledDate     
      
     
  select * from #temp order by cast(pslno as int ),cast(subslno as int),cast(mixslNo as int),settledDate    
    
GO




"
           );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
