using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Masters.Exporting
{
    public class IndexNamesExcelExporter : NpoiExcelExporterBase, IIndexNamesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public IndexNamesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetIndexNameForViewDto> indexNames)
        {
            return CreateExcelPackage(
                "IndexNames.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.CreateSheet(L("IndexNames"));

                    AddHeader(
                        sheet,
                        L("Name")
                        );

                    AddObjects(
                        sheet, 2, indexNames,
                        _ => _.IndexName.Name
                        );

					
					
                });
        }
    }
}
