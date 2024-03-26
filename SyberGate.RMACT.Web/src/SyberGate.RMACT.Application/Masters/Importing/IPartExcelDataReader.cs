using System.Collections.Generic;
using SyberGate.RMACT.Masters.Importing.Dto;
using Abp.Dependency;

namespace SyberGate.RMACT.Masters.Importing
{
    public interface IPartExcelDataReader : ITransientDependency
    {
        List<ImportPartDto> GetPartsFromExcel(byte[] fileBytes);
    }

}

