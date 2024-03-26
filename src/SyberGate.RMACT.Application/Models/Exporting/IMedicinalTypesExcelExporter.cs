using System.Collections.Generic;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Models.Exporting
{
    public interface IMedicinalTypesExcelExporter
    {
        FileDto ExportToFile(List<GetMedicinalTypeForViewDto> medicinalTypes);
    }
}