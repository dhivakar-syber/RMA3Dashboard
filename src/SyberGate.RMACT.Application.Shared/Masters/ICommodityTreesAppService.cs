using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;

namespace SyberGate.RMACT.Masters
{
    public interface ICommodityTreesAppService : IApplicationService
    {
        Task<ListResultDto<CommodityTreeDto>> GetCommodityTrees();

        Task<CommodityTreeDto> CreateCommodityTree(CreateCommodityTreeInput input);

        Task<CommodityTreeDto> UpdateCommodityTree(UpdateCommodityTreeInput input);

        Task<CommodityTreeDto> MoveCommodityTree(MoveCommodityTreeInput input);

        Task DeleteCommodityTree(EntityDto<long> input);

    }
}
