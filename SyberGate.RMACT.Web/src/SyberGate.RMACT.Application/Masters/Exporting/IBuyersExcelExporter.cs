using System.Collections.Generic;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Masters.Exporting
{
    public interface IBuyersExcelExporter
    {
        FileDto ExportToFile(List<GetBuyerForViewDto> buyers);
    }
}