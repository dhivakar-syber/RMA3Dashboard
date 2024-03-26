using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Masters.Exporting
{
    public class RawMaterialIndexesExcelExporter : NpoiExcelExporterBase, IRawMaterialIndexesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public RawMaterialIndexesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetRawMaterialIndexForViewDto> rawMaterialIndexes)
        {
            return CreateExcelPackage(
                "RawMaterialIndexes.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.CreateSheet(L("RawMaterialIndexes"));

                    AddHeader(
                        sheet,
                        L("Month"),
                        L("Value"),
                        (L("IndexName")) + L("Name"),
                        (L("Year")) + L("Name"),
                        (L("RawMaterialGrade")) + L("Name")
                        );

                    AddObjects(
                        sheet, 2, rawMaterialIndexes,
                        _ => _.RawMaterialIndex.Month,
                        _ => _.RawMaterialIndex.Value,
                        _ => _.IndexNameName,
                        _ => _.YearName,
                        _ => _.RawMaterialGradeName
                        );

					
					
                });
        }
    }
}
