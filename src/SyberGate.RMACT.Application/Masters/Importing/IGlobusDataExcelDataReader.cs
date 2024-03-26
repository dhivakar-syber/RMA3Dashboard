using System.Collections.Generic;
using SyberGate.RMACT.Masters.Importing.Dto;
using Abp.Dependency;

namespace SyberGate.RMACT.Masters.Importing
{
    public interface IGlobusDataExcelDataReader : ITransientDependency
    {
        List<ImportGlobusDataDto> GetGlobusDatasFromExcel(byte[] fileBytes);
    }

}

