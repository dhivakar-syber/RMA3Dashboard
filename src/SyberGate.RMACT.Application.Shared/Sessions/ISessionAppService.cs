using System.Threading.Tasks;
using Abp.Application.Services;
using SyberGate.RMACT.Sessions.Dto;

namespace SyberGate.RMACT.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
