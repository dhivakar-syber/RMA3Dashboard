using System.Collections.Generic;
using SyberGate.RMACT.Masters.Importing.Dto;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Masters.Importing
{
    public interface IInvalidGlobusDataExporter
    {
        FileDto ExportToFile(List<ImportGlobusDataDto> globusDataListDtos);
    }
}
