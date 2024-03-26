using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Authorization.Users.Dto;

namespace SyberGate.RMACT.Authorization.Users
{
    public interface IUserLinkAppService : IApplicationService
    {
        Task LinkToUser(LinkToUserInput linkToUserInput);

        Task<PagedResultDto<LinkedUserDto>> GetLinkedUsers(GetLinkedUsersInput input);

        Task<ListResultDto<LinkedUserDto>> GetRecentlyUsedLinkedUsers();

        Task UnlinkUser(UnlinkUserInput input);
    }
}
