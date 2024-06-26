using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Models.Exporting
{
    public class MedicalSpecialtiesExcelExporter : NpoiExcelExporterBase, IMedicalSpecialtiesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MedicalSpecialtiesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMedicalSpecialtyForViewDto> medicalSpecialties)
        {
            return CreateExcelPackage(
                "MedicalSpecialties.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("MedicalSpecialties"));

                    AddHeader(
                        sheet,
                        L("Code"),
                        L("Name"),
                        L("Fullname"),
                        L("DecisionCode"),
                        L("Status")
                        );

                    AddObjects(
                        sheet, 2, medicalSpecialties,
                        _ => _.MedicalSpecialty.Code,
                        _ => _.MedicalSpecialty.Name,
                        _ => _.MedicalSpecialty.Fullname,
                        _ => _.MedicalSpecialty.DecisionCode,
                        _ => _.MedicalSpecialty.Status
                        );

                });
        }
    }
}