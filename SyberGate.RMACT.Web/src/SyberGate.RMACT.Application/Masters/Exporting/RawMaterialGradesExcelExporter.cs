using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Masters.Exporting
{
    public class RawMaterialGradesExcelExporter : NpoiExcelExporterBase, IRawMaterialGradesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public RawMaterialGradesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetRawMaterialGradeForViewDto> rawMaterialGrades)
        {
            return CreateExcelPackage(
                "RawMaterialGrades.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.CreateSheet(L("RawMaterialGrades"));

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("IsGroup"),
                        L("HasMixture"),
                        (L("RawMaterialGrade")) + L("Name")
                        );

                    AddObjects(
                        sheet, 2, rawMaterialGrades,
                        _ => _.RawMaterialGrade.Name,
                        _ => _.RawMaterialGrade.IsGroup,
                        _ => _.RawMaterialGrade.HasMixture,
                        _ => _.RawMaterialGradeName
                        );

					
					
                });
        }
    }
}
