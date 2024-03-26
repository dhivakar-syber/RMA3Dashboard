using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Tenants.Dashboard.Dto;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;
using SyberGate.RMACT.Masters.Dtos;

namespace SyberGate.RMACT.Masters.Exporting
{
    public class A3ExcelExporter : NpoiExcelExporterBase, IA3ExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public A3ExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetRMPriceTrend> priceTrend, List<GetRMPriceImpact> priceImpact, List<LeadModelDto> LeadModeLHeader, List<PartLeadModelMatrixDto> PartMod,List<LeadModelGraphDto>LeadP, int BuyPartNm, string Supplier, string Buyer, string TemplatePath, A3Document A3Doc, decimal RmTotal,decimal processImpacttotal,decimal basermimpacttotal)
        {
            var ValueCount = priceTrend.Count;

			var version = "";
            var remarks = "";
            if (A3Doc != null)
            {
                version = A3Doc.Version;
                remarks = A3Doc.Remarks;
            }

            return CreateExcelPackageFromFile(
                "RMA3Sheet-" + version + ".xlsx",
                 TemplatePath,
                excelPackage =>
                {
                    
                    var sheet = excelPackage.GetSheetAt(0);

                    AddObjects(
                        sheet, 15, 1, priceTrend,
                        _=> _.ParentGrp,
                        _ => _.MixtureGrade,
                        _ => _.RMGrade,
                        _ => _.Uom,
                        _ => _.SetteledMY,
                        _ => _.SetteledUR,
                        _ => _.SetteledWRatio,
                        _ => _.SetteledLRatio,
                        _ => _.RevisedMY,
                        _ => _.RevisedUR,
                        _ => _.RevisedWRatio,
                        _ => _.RevisedLRatio,
                        _ => _.BaseRMPOC,
                        _ => _.ScrapSetteled,
                        _ => _.ScrapRevised,
                        _ => _.ScrapPOC
                        );
                    ValueCount= ValueCount+18;

					AddLeadModelObjects(sheet,ValueCount, 0, LeadModeLHeader,
                        _ => _.Name,
                        _ => _.Description

                       
                        );
                    
                    ValueCount= ValueCount+3;

					AddPartModelObjects(sheet, ValueCount, 0, PartMod,BuyPartNm,

                        _ => _.PartNo,
                        _ => _.PartDespn,
                        _ => _.LeadModelId,
                        _ => _ .Quantity



						);

                    if(BuyPartNm==0)
                        ValueCount = ValueCount + BuyPartNm + 6;
                    else
                        ValueCount = ValueCount + BuyPartNm + 5;


                    AddObjectsNew(sheet,ValueCount, 0, priceImpact, priceImpact, RmTotal, processImpacttotal, basermimpacttotal, 
                        _ => _.SlNo,
                        _ => _.PartNo,
                        _ => _.Description,
                        _ => _.PlantCode,
                        _ => _.ES1,
                        _ => _.ES2,
                        _ => _.RawMaterialGroup,
                        _ => _.RawMaterialGrade,
                        _ => _.CurrentCostPer,
                        _ => _.BaseRMRate,
                        _ => _.RMSurchargeGradeDiff,
                        _ => _.SecondaryProcessing,
                        _ => _.SurfaceProtection,
                        _ => _.Thickness,
                        _ => _.CuttingCost,
                        _ => _.MOQVolume,
                        _ => _.Transport,
                        _ => _.Others,
                        _ => _.RevisedCostPer,
                        _ => _.RevBaseRMRate,
                        _ => _.RevRMSurchargeGradeDiff,
                        _ => _.RevSecondaryProcessing,
                        _ => _.RevSurfaceProtection,
                        _ => _.RevThickness,
                        _ => _.RevCuttingCost,
                        _ => _.RevMOQVolume,
                        _ => _.RevTransport,
                        _ => _.RevOthers,
                        _ => _.GrossInputWeight,
                        _ => _.CastingForgingWeight,
                        _ => _.FinishedWeight,
                        _ => _.ScrapRecovery,
                        _ => _.ScrapRecoveryPercent,
                        _ => _.ScrapWeight,
                        _ => _.CurrentRMCost,
                        _ => _.RevisedRMCost,
                        _ => _.OtherCost,
                        _ => _.CurrentExwPrice,
                        _ => _.RevisedExwPrice,
                        _ => _.ExwPriceChangeInCost,
                        _ => _.ExwPriceChangeInPer,
                        _ => _.PackagingCost,
                        _ => _.LogisticsCost,
                        _ => _.CurrentFCAPrice,
                        _ => _.RevisedFCAPrice,
                        _ => _.CurrentAVOB,
                        _ => _.RevisedAVOB,
                        _ => _.SOB,
                        _ => _.GlobusEPU,
                        _ => _.RMImpact,
                        _ => _.RMReference
                        );

                    var row = sheet.GetRow(4);
                    var cell = row.GetCell(7);
                    cell.SetCellValue(Supplier);


                    var row1= sheet.GetRow(5);

                    var cell1 = row1.GetCell(7);

                    cell1.SetCellValue(Buyer);


                    


                    var row26 = sheet.GetRow(ValueCount + priceImpact.Count + 3);
                    var cell26 = row26.GetCell(2);
                    cell26.SetCellValue(remarks);

                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(row26.RowNum, row26.RowNum, 0, 1));
                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(row26.RowNum, row26.RowNum+1, 2, 20));

                    ValueCount = ValueCount + priceImpact.Count + 9;

                    AddLeadModelGraphObjects(sheet, ValueCount, 0, LeadP,
                        _ => _.LeadModelName,
                        _ => _.CurrentRM,
                        _=> _.RevisedRM


                        );
                });
        }
    }
}
