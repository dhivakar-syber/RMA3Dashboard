using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Models.Exporting
{
    public class ConstantsExcelExporter : NpoiExcelExporterBase, IConstantsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ConstantsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetConstantForViewDto> constants)
        {
            return CreateExcelPackage(
                "Constants.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Constants"));

                    AddHeader(
                        sheet,
                        L("Code"),
                        L("Name"),
                        L("Description"),
                        L("Type")
                        );

                    AddObjects(
                        sheet, 2, constants,
                        _ => _.Constant.Code,
                        _ => _.Constant.Name,
                        _ => _.Constant.Description,
                        _ => _.Constant.Type
                        );

                });
        }
    }
}