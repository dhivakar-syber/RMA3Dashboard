using System.Collections.Generic;
using SyberGate.RMACT.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace SyberGate.RMACT.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
