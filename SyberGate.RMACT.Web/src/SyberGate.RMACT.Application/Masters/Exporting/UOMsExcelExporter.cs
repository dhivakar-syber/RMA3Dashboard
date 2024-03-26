using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Masters.Exporting
{
    public class UOMsExcelExporter : NpoiExcelExporterBase, IUOMsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public UOMsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetUOMForViewDto> uoMs)
        {
            return CreateExcelPackage(
                "UOMs.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.CreateSheet(L("UOMs"));

                    AddHeader(
                        sheet,
                        L("Code"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, uoMs,
                        _ => _.UOM.Code,
                        _ => _.UOM.Description
                        );

					
					
                });
        }
    }
}
