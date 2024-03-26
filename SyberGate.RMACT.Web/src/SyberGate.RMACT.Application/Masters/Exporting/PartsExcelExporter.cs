using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Masters.Exporting
{
    public class PartsExcelExporter : NpoiExcelExporterBase, IPartsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PartsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPartForViewDto> parts)
        {
            return CreateExcelPackage(
                "Parts.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.CreateSheet(L("Parts"));

                    AddHeader(
                        sheet,
                        L("GrossInputWeight"),
                        L("CastingForgingWeight"),
                        L("FinishedWeight"),
                        L("ScrapRecoveryPercent"),
                        L("PartNo"),
                        L("Description"),
                        (L("Supplier")) + L("Name"),
                        (L("Buyer")) + L("Name"),
                        (L("RMGroup")) + L("Name")
                        );

                    AddObjects(
                        sheet, 2, parts,
                        _ => _.Part.GrossInputWeight,
                        _ => _.Part.CastingForgingWeight,
                        _ => _.Part.FinishedWeight,
                        _ => _.Part.ScrapRecoveryPercent,
                        _ => _.Part.PartNo,
                        _ => _.Part.Description,
                        _ => _.SupplierName,
                        _ => _.BuyerName,
                        _ => _.RMGroupName
                        );

					
					
                });
        }
    }
}
