using System.Collections.Generic;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Masters.Exporting
{
    public interface IUOMsExcelExporter
    {
        FileDto ExportToFile(List<GetUOMForViewDto> uoMs);
    }
}