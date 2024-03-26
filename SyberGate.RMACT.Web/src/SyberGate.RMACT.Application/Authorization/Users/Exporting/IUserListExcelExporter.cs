using System.Collections.Generic;
using SyberGate.RMACT.Authorization.Users.Dto;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}