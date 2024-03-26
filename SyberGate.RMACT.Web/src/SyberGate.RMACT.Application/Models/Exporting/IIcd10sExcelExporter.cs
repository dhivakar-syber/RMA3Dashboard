using System.Collections.Generic;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Models.Exporting
{
    public interface IIcd10sExcelExporter
    {
        FileDto ExportToFile(List<GetIcd10ForViewDto> icd10s);
    }
}