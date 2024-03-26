using System.Collections.Generic;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Masters.Exporting
{
    public interface IRawMaterialIndexesExcelExporter
    {
        FileDto ExportToFile(List<GetRawMaterialIndexForViewDto> rawMaterialIndexes);
    }
}