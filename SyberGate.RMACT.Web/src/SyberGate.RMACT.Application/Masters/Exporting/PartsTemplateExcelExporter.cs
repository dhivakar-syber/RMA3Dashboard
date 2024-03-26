using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using SyberGate.RMACT.DataExporting.Excel.NPOI;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Masters.Exporting
{
    public class PartsTemplateExcelExporter : NpoiExcelExporterBase, IPartsTemplateExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PartsTemplateExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPartForViewDto> parts, List<GetRawMaterialGradeForViewDto> rmGrades, List<GetRawMaterialGradeForViewDto> rmGroup, string supplier, string buyer, string templatePath)
        {
            return CreateExcelPackageFromFile(
                "PartsUploadTemplate.xlsx",
                templatePath,
                excelPackage =>
                {
                    var sheetParts = excelPackage.GetSheetAt(0);

                    var row1 = sheetParts.GetRow(2);
                    var cell1 = row1.GetCell(2);
                    cell1.SetCellValue(buyer);

                    var row = sheetParts.GetRow(3);
                    var cell = row.GetCell(2);
                    cell.SetCellValue(supplier);

                    var row5 = sheetParts.GetRow(5);
                    var cell5 = row5.GetCell(0);

                    var sheet = excelPackage.GetSheetAt(1);
                    
                    AddHeader(
                        sheet,
                        L("PartNo"),
                        L("Description"),
                        L("SubPartNo"),
                        L("SubPartDescription"),
                        (L("RMGrade")) + L("Name"),
                        (L("RMSpec")) + L("Name"),
                        L("GrossInputWeight"),
                        L("CastingForgingWeight"),
                        L("FinishedWeight"),
                        L("ScrapRecoveryPercent"),
                        L("RMReferenceCost")
                        );

                    AddObjects(
                        sheet, 2, parts,
                        _ => _.Part.PartNo,
                        _ => _.Part.Description,
                        _ => _.Part.SubPartNo,
                        _ => _.Part.SubPartDescription,
                        _ => _.RMGroupName,
                        _ => _.RMGradeName,
                        _ => _.Part.GrossInputWeight,
                        _ => _.Part.CastingForgingWeight,
                        _ => _.Part.FinishedWeight,
                        _ => _.Part.ScrapRecoveryPercent,
                        _ => _.Part.RMReferenceCost,
                        _ => _.Part.RMReference
                        );

                    var RMGroupSheet = excelPackage.GetSheetAt(2);

                    AddHeader(
                        RMGroupSheet,
                        L("Id"),
                        (L("RMGrade")) + L("Name")
                        );

                    AddObjects(
                        RMGroupSheet, 2, rmGroup,
                        _ => _.RawMaterialGrade.Id,
                        _ => _.RawMaterialGrade.Name
                        );

                    var RMGradeSheet = excelPackage.GetSheetAt(3);

                    AddHeader(
                        RMGradeSheet,
                        L("Id"),
                        (L("RMSpec")) + L("Name"),
                        (L("RMGrade")) + L("Name")
                        );

                    AddObjects(
                        RMGradeSheet, 2, rmGrades,
                        _ => _.RawMaterialGrade.Id,
                        _ => _.RawMaterialGrade.Name,
                        _ => _.RawMaterialGrade.RMGroupName
                        );
                });
        }
    }
}
