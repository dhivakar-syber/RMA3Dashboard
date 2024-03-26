using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Models.Exporting
{
    public class EthnicitiesExcelExporter : NpoiExcelExporterBase, IEthnicitiesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public EthnicitiesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetEthnicityForViewDto> ethnicities)
        {
            return CreateExcelPackage(
                "Ethnicities.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.CreateSheet(L("Ethnicities"));

                    AddHeader(
                        sheet,
                        L("Code"),
                        L("Name"),
                        L("Description"),
                        L("Status"),
                        L("IsDeleted")
                        );

                    AddObjects(
                        sheet, 2, ethnicities,
                        _ => _.Ethnicity.Code,
                        _ => _.Ethnicity.Name,
                        _ => _.Ethnicity.Description,
                        _ => _.Ethnicity.Status,
                        _ => _.Ethnicity.IsDeleted
                        );

					
					
                });
        }
    }
}
