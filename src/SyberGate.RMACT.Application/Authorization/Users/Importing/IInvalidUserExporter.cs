using System.Collections.Generic;
using SyberGate.RMACT.Authorization.Users.Importing.Dto;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
