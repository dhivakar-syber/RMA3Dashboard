using System.Collections.Generic;
using SyberGate.RMACT.Masters.Importing.Dto;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Masters.Importing
{
    public interface IInvalidPartExporter
    {
        FileDto ExportToFile(List<ImportPartDto> partListDtos);
    }
}
