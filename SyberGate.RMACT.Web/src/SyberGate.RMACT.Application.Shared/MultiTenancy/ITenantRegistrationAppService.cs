using System.Threading.Tasks;
using Abp.Application.Services;
using SyberGate.RMACT.Editions.Dto;
using SyberGate.RMACT.MultiTenancy.Dto;

namespace SyberGate.RMACT.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}