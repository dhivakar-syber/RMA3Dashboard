using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Caching.Dto;

namespace SyberGate.RMACT.Caching
{
    public interface ICachingAppService : IApplicationService
    {
        ListResultDto<CacheDto> GetAllCaches();

        Task ClearCache(EntityDto<string> input);

        Task ClearAllCaches();
    }
}
