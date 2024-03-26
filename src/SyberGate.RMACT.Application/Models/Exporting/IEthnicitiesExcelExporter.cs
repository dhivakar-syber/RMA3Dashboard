using System.Collections.Generic;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Models.Exporting
{
    public interface IEthnicitiesExcelExporter
    {
        FileDto ExportToFile(List<GetEthnicityForViewDto> ethnicities);
    }
}