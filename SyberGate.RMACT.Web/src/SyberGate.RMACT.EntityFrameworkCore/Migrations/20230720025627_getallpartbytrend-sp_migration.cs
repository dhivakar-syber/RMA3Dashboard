﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class getallpartbytrendsp_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
          @"
Alter procedure GetAllPartByTrend                                        
@SupplierId varchar(255) null,                                        
@BuyerId varchar(255) null,                                        
@Period varchar(255) null,                                        
@IsGenerateA3 bit,                                        
@A3Id int null,                                        
@GradeId varchar(255) null,                                            
@SpecId varchar(255) null                                         
AS                                        
                                        
declare @startdate date                                        
declare @enddate date                                        
set @startdate = convert(date,convert(varchar, SUBSTRING(@Period, 1, 10) , 101),101)                                        
set @enddate = convert(date,convert(varchar, SUBSTRING(@Period, 13, 11) , 101),101)                                        
-----------------------------------------------------------------------------------------------          
--parts 0          
select distinct isnull(g.Name,'') MixtureGrade, a.Id, a.RMGroupId, isnull(grd.Name,'') [RMGrade], isnull(u.Name, 'Kilogram') UOM,                                 
a.SetteledUnitRate SRate, a.SetteledScrapPer SScrap, a.RevisedUnitRate RRate, a.RevisedScrapPer RScrap, cast((case a.SetteledUnitRate when 0 then 0                                 
else (a.RevisedUnitRate - a.SetteledUnitRate )/ a.SetteledUnitRate end ) * 100 as decimal(18,5))  RMpchage,                                        
cast((case a.SetteledScrapPer when 0 then 0 else (a.RevisedScrapPer - a.SetteledScrapPer )/ a.SetteledScrapPer end  ) * 100 as decimal(18,5)) SPchange,                                         
a.reviseddate as RevisedMY,                                        
a.settleddate as SetteledMY,                            
a.RevApproved, a.SetApproved, a.SetId, grd.Id RMGradeId, isnull(u.Id,1) UOMId, a.RevIndexName, a.RevIndexValue, a.RevFromPeriod, a.RevToPeriod, a.SetIndexName, a.SetIndexValue, a.SetFromPeriod, a.SetToPeriod,                                        
a.swRatio, a.slRatio, a.rwRatio, a.rlRatio, grd.HasMixture,isnull(prntgrp.Name,'')  ParentGroup                         
                        
into #temptrend                                        
                        
from parts p                                        
left outer join vw_rmtrend a on p.RMGradeId = a.RMGroupId and a.Id = (select top 1 (b.Id) from vw_rmtrend b where a.rmgroupid = b.rmgroupid and b.BuyerId = p.BuyerId and p.SupplierId = b.SupplierId                                        
and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)) order by b.reviseddate desc)                                        
                                        
inner join Suppliers s on p.SupplierId = s.Id                                        
inner join Buyers buy on p.BuyerId = buy.id                                        
inner join RawMaterialGrades g on g.Id = p.rmgroupid                                        
inner join RawMaterialGrades grd on grd.id = p.RMGradeId                                        
left outer join UnitOfMeasurements u on u.Id = a.UnitOfMeasurementId              
left outer join RawMaterialGrades gm on gm.id=p.rmgroupid            
left outer join RawMaterialGrades prntgrp on prntgrp.id=gm.RawMaterialGradeId             
            
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId and grd.HasMixture = 0                                         
       and case when isnull(@GradeId, 0) = 0 then 1 else @GradeId end = case when isnull(@GradeId, 0) = 0 then 1 else p.RMGroupId end   
    and case when isnull(@SpecId, 0) = 0 then 1 else @SpecId end = case when isnull(@SpecId, 0) = 0 then 1 else p.RMGradeId end--and (a.settleddate between @startdate and @enddate)                                         
-----------------------------------------------------------------------------------------------------------------------                                        
 --parts 1                                       
insert into #temptrend                                        
select distinct isnull(prntgrp.Name,'') MixtureGrade,  a.Id, a.RMGroupId,                                
isnull(g.Name,'') [RMGrade], isnull(u.Name, 'Kilogram') UOM, a.SetteledUnitRate SRate, a.SetteledScrapPer SScrap, a.RevisedUnitRate RRate, a.RevisedScrapPer RScrap, cast((case a.SetteledUnitRate when 0 then                                 
0 else (a.RevisedUnitRate - a.SetteledUnitRate )/ a.SetteledUnitRate end ) * 100 as decimal(18,5))  RMpchage,                                        
cast((case a.SetteledScrapPer when 0 then 0 else (a.RevisedScrapPer - a.SetteledScrapPer )/ a.SetteledScrapPer end  ) * 100 as decimal(18,5)) SPchange,                                         
a.reviseddate as RevisedMY,                                        
a.settleddate as SetteledMY,                                        
a.RevApproved, a.SetApproved, a.SetId, g.Id RMGradeId, isnull(u.Id,1) UOMId, a.RevIndexName, a.RevIndexValue, a.RevFromPeriod, a.RevToPeriod, a.SetIndexName, a.SetIndexValue, a.SetFromPeriod, a.SetToPeriod,                                        
a.swRatio, a.slRatio, a.rwRatio, a.rlRatio, grd.HasMixture,isnull(gm.Name,'')  ParentGroup                                     
from parts p                                        
inner join Suppliers s on p.SupplierId = s.Id                                        
inner join Buyers buy on p.BuyerId = buy.id                         
inner join RawMaterialMixtures m on m.SupplierId = s.Id and m.BuyerId = buy.Id and m.RMGroupId = p.RMGradeId                                        
inner join RawMaterialGrades grd on grd.id = p.RMGradeId                                   
inner join RawMaterialGrades gm on gm.Id = p.RMGroupId                                        
inner join RawMaterialGrades g on g.Id = m.RawMaterialGradeId              
left outer join RawMaterialGrades prntgrp on prntgrp.id=g.RawMaterialGradeId              
                                        
left outer join vw_rmtrend a on g.Id = a.RMGroupId and                                         
a.Id = (select top 1 (b.Id) from vw_rmtrend b where a.rmgroupid = b.rmgroupid and b.BuyerId = p.BuyerId and p.SupplierId = b.SupplierId                                        
and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)) order by reviseddate desc)                                        
                                        
left outer join UnitOfMeasurements u on u.Id = a.UnitOfMeasurementId                                        
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId and grd.HasMixture = 1 --and (a.settleddate between @startdate and @enddate)                                        
       and case when isnull(@GradeId, 0) = 0 then 1 else @GradeId end = case when isnull(@GradeId, 0) = 0 then 1 else p.RMGroupId end  
    and case when isnull(@SpecId, 0) = 0 then 1 else @SpecId end = case when isnull(@SpecId, 0) = 0 then 1 else p.RMGradeId end                                        
---------------------------------------------------------------------------------------------------------------------------------------------                                        
--subParts 0          
insert into #temptrend                                        
                
select distinct isnull(g.Name,'') MixtureGrade, a.Id, a.RMGroupId, isnull(grd.Name,'') [RMGrade], isnull(u.Name, 'Kilogram') UOM,                                 
a.SetteledUnitRate SRate, a.SetteledScrapPer SScrap, a.RevisedUnitRate RRate, a.RevisedScrapPer RScrap, cast((case a.SetteledUnitRate when 0 then 0                                 
else (a.RevisedUnitRate - a.SetteledUnitRate )/ a.SetteledUnitRate end ) * 100 as decimal(18,5))  RMpchage,                                        
cast((case a.SetteledScrapPer when 0 then 0 else (a.RevisedScrapPer - a.SetteledScrapPer )/ a.SetteledScrapPer end  ) * 100 as decimal(18,5)) SPchange,                                         
a.reviseddate as RevisedMY,                                        
a.settleddate as SetteledMY,                            
a.RevApproved, a.SetApproved, a.SetId, grd.Id RMGradeId, isnull(u.Id,1) UOMId, a.RevIndexName, a.RevIndexValue, a.RevFromPeriod, a.RevToPeriod, a.SetIndexName, a.SetIndexValue, a.SetFromPeriod, a.SetToPeriod,                                        
a.swRatio, a.slRatio, a.rwRatio, a.rlRatio,  0 HasMixture ,isnull(prntgrp.Name,'') ParentGroup                        
                  
                  
                  
from SubParts p                   
                
left outer join vw_rmtrend a on p.RMGradeId = a.RMGroupId and a.Id = (select top 1 (b.Id) from vw_rmtrend b where a.rmgroupid = b.rmgroupid and b.BuyerId = p.BuyerId and p.SupplierId = b.SupplierId                                        
and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)) order by b.reviseddate desc)                  
                  
inner join Suppliers s on p.SupplierId = s.Id                                        
inner join Buyers buy on p.BuyerId = buy.id                                        
inner join RawMaterialGrades g on g.Id = p.rmgroupid                     
inner join RawMaterialGrades grd  on grd.Id = p.RMGradeId                     
left outer join UnitOfMeasurements u on u.Id = a.UnitOfMeasurementId              
left outer join RawMaterialGrades prntgrp on prntgrp.id=g.RawMaterialGradeId             
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId and grd.HasMixture = 0--and (a.settleddate between @startdate and @enddate)                                        
--and p.ParentPartNo in (select distinct PartNo from #temptrend)                                        
       and case when isnull(@GradeId, 0) = 0 then 1 else @GradeId end = case when isnull(@GradeId, 0) = 0 then 1 else p.rmgroupid end  
    and case when isnull(@SpecId, 0) = 0 then 1 else @SpecId end = case when isnull(@SpecId, 0) = 0 then 1 else p.rmgroupid end                
                  
--------------------------------------------------------------.>>>>>>>>>>>>>>>>>>>>>>>>              
--subparts 1          
insert into #temptrend              
              
select distinct isnull(prntgrp.Name,'') MixtureGrade,  a.Id, a.RMGroupId,                                
isnull(g.Name,'') [RMGrade], isnull(u.Name, 'Kilogram') UOM, a.SetteledUnitRate SRate, a.SetteledScrapPer SScrap, a.RevisedUnitRate RRate, a.RevisedScrapPer RScrap, cast((case a.SetteledUnitRate when 0 then                                 
0 else (a.RevisedUnitRate - a.SetteledUnitRate )/ a.SetteledUnitRate end ) * 100 as decimal(18,5))  RMpchage,                                        
cast((case a.SetteledScrapPer when 0 then 0 else (a.RevisedScrapPer - a.SetteledScrapPer )/ a.SetteledScrapPer end  ) * 100 as decimal(18,5)) SPchange,                                         
a.reviseddate as RevisedMY,                                        
a.settleddate as SetteledMY,                                        
a.RevApproved, a.SetApproved, a.SetId, g.Id RMGradeId, isnull(u.Id,1) UOMId, a.RevIndexName, a.RevIndexValue, a.RevFromPeriod, a.RevToPeriod, a.SetIndexName, a.SetIndexValue, a.SetFromPeriod, a.SetToPeriod,                                        
a.swRatio, a.slRatio, a.rwRatio, a.rlRatio, grd.HasMixture,isnull(gm.Name,'')  ParentGroup                                      
from SubParts p                                        
inner join Suppliers s on p.SupplierId = s.Id                                        
inner join Buyers buy on p.BuyerId = buy.id                         
inner join RawMaterialMixtures m on m.SupplierId = s.Id and m.BuyerId = buy.Id and m.RMGroupId = p.RMGradeId                                        
inner join RawMaterialGrades grd on grd.id = p.RMGradeId                                   
inner join RawMaterialGrades gm on gm.Id = p.RMGroupId                                        
inner join RawMaterialGrades g on g.Id = m.RawMaterialGradeId              
left outer join RawMaterialGrades prntgrp on prntgrp.id=g.RawMaterialGradeId              
                                        
left outer join vw_rmtrend a on g.Id = a.RMGroupId and                                         
a.Id = (select top 1 (b.Id) from vw_rmtrend b where a.rmgroupid = b.rmgroupid and b.BuyerId = p.BuyerId and p.SupplierId = b.SupplierId                
and ((b.reviseddate >= @startdate and b.reviseddate <= @enddate) or (b.reviseddate <= @startdate and b.reviseddate <= @enddate)) order by reviseddate desc)                                        
                                        
left outer join UnitOfMeasurements u on u.Id = a.UnitOfMeasurementId                                        
where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId and grd.HasMixture = 1 --and (a.settleddate between @startdate and @enddate)                                   
       and case when isnull(@GradeId, 0) = 0 then 1 else @GradeId end = case when isnull(@GradeId, 0) = 0 then 1 else p.RMGroupId end  
    and case when isnull(@SpecId, 0) = 0 then 1 else @SpecId end = case when isnull(@SpecId, 0) = 0 then 1 else p.RMGradeId end             
----------------------------------------------------------------<<<<<<<<<<<<<<<<<<<<<<<              
                                        
if (@IsGenerateA3 = 1)                                        
                                      
Begin                                        
      --insert into A3PartBuckets(DocId,RMSpec,Buckets,[Value],buyer,supplier,CreatedOn) select @A3Id,RMSpec,Buckets,[Value],buyer,supplier,CreatedOn from PartBuckets                               
        --insert into A3RMTapTool(DocId,RMGrade,RMSpec,buyer,supplier,BaseRMRate,RMSurchargeGradeDiff,SecondaryProcessing,SurfaceProtection,Thickness,MOQVolume,CuttingCost,
		--Transport,Others,BuyerId,SupplierId,CreatedOn,date)        
      --select @A3Id,rmspec,RMSpec,buyer,supplier,BaseRMRate,RMSurchargeGradeDiff,SecondaryProcessing,SurfaceProtection,Thickness,MOQVolume,CuttingCost,Transport,
	  --Others,BuyerId,SupplierId,CreatedOn,date from RMTapTool where BuyerId=@BuyerId and SupplierId=  
  --@SupplierId                          
      insert into A3LeadModels(DocId, [Name], [Description],LeadModelId) select @A3Id, [Name], [Description],Id from LeadModels                                           
                                
      insert into A3PartModelMatrixes (DocId, PartNumber, [Description], Quantity, LeadModelId)                                          
              select @A3Id, p.PartNo, p.[Description], mm.Quantity, mm.LeadModelId from PartModelMatrixes mm inner join Parts p on mm.PartNumber = p.PartNo                                           
              where p.BuyerId = @BuyerId and p.SupplierId = @SupplierId order by p.PartNo, mm.LeadModelId                                       
                                      
       insert into A3PriceTrends (DocId, RevId, RMGroupId, RMGrade, Uom, SetteledMY, SetteledUR, RevisedMY, RevisedUR, BaseRMPOC, ScrapSetteled, ScrapRevised, ScrapPOC, RevApproved,                                        
             SetApproved, SetId, RevFromPeriod, RevIndexName, RevIndexValue, RevToPeriod, mixtureGrade, SetteledWRatio, SetteledLRatio, RevisedWRatio, RevisedLRatio, HasMixture                                        
    )                                         
       select distinct @A3Id, Id, RMGroupId, RMGrade, UOM, SetteledMY, SRate, RevisedMY, RRate, RMpchage,                                         
             SScrap, RScrap, SPchange, RevApproved, SetApproved, SetId, RevFromPeriod, RevIndexName, RevIndexValue, RevToPeriod, MixtureGrade, swRatio, slRatio, rwRatio, rlRatio, HasMixture                                          
    from #temptrend                                        
                                      
 end                                       
select distinct  * from #temptrend                                       
                                      
--group by                                       
--Id,MixtureGrade,RMGroupId,RMGrade,uom,SRate,SScrap,RRate,RScrap,RMpchage,SPchange,RevisedMY,SetteledMY,RevApproved,SetApproved,SetId,                                      
--RMGrade,UOMId,RevIndexName,RevFromPeriod,RevToPeriod,SetIndexName,SetIndexValue,SetFromPeriod,SetToPeriod,                                      
--swRatio,slRatio,rwRatio,rlRatio,HasMixture,RMGradeId,RevIndexValue                                      
order by SetteledMY,RevisedMY  
  
  
  
  

"
          );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
