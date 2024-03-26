using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Authorization.Permissions.Dto;

namespace SyberGate.RMACT.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
