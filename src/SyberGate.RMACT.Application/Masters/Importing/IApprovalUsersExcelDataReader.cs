using Abp.Dependency;
using SyberGate.RMACT.Masters.Importing.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Importing
{
    
     public interface IApprovalUsersExcelDataReader : ITransientDependency
    {
        List<ImportApprovalUsersDto> GetApprovalUserFromExcel(byte[] fileBytes);
    }
}
