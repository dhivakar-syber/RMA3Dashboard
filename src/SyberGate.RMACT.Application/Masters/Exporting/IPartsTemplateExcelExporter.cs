using System.Collections.Generic;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Masters.Exporting
{
    public interface IPartsTemplateExcelExporter
    {
        FileDto ExportToFile(List<GetPartForViewDto> Parts, List<GetRawMaterialGradeForViewDto> RMGrades, List<GetRawMaterialGradeForViewDto> RMGroup, string Supplier, string Buyer, string TemplatePath);
    }
}