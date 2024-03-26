

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Organizations.Dto;
using SyberGate.RMACT.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace SyberGate.RMACT.Masters
{
    [AbpAuthorize(AppPermissions.Pages_CommodityTrees)]
    public class CommodityTreesAppService : RMACTAppServiceBase, ICommodityTreesAppService
    {
        private readonly CommodityTreeManager _commodityTreeManager;
        private readonly IRepository<CommodityTree, long> _commodityTreeRepository;
        private readonly IRepository<RawMaterialGrade> _RawMaterialgradeRepository;


        public CommodityTreesAppService(CommodityTreeManager commodityTreeManager,
            IRepository<CommodityTree, long> commodityTreeRepository,IRepository<RawMaterialGrade> RawMaterialRepository
            )
        {
            _commodityTreeManager = commodityTreeManager;
            _commodityTreeRepository = commodityTreeRepository;
            _RawMaterialgradeRepository= RawMaterialRepository;

        }

        public async Task<ListResultDto<CommodityTreeDto>> GetCommodityTrees()
        {
            var commodityTrees = await _commodityTreeRepository.GetAllListAsync();

            return new ListResultDto<CommodityTreeDto>(
                commodityTrees.Select(ou =>
                {
                    var CommodityTreeDto = ObjectMapper.Map<CommodityTreeDto>(ou);
                    return CommodityTreeDto;
                }).ToList());
        }


        public async Task<ListResultDto<RawMaterialGradeDto>> GetRawMaterialGrades()
        {
            var RawMaterialGrades = await _RawMaterialgradeRepository.GetAllListAsync();

            return new ListResultDto<RawMaterialGradeDto>(
                RawMaterialGrades.Select(ou =>

                {
                    var RawMaterialGradeDto = ObjectMapper.Map<RawMaterialGradeDto>(ou);

                    return RawMaterialGradeDto;

                }).ToList());



        }

        [AbpAuthorize(AppPermissions.Pages_CommodityTrees_ManageCommodityTree)]
        public async Task<CommodityTreeDto> CreateCommodityTree(CreateCommodityTreeInput input)
        {
            var commodityTree = new CommodityTree(AbpSession.TenantId, input.DisplayName, input.ParentId, input.IsLeaf);

            await _commodityTreeManager.CreateAsync(commodityTree);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<CommodityTreeDto>(commodityTree);
        }

        [AbpAuthorize(AppPermissions.Pages_CommodityTrees_ManageCommodityTree)]
        public async Task<CommodityTreeDto> UpdateCommodityTree(UpdateCommodityTreeInput input)
        {
            var commodityTree = await _commodityTreeRepository.GetAsync(input.Id);

            commodityTree.DisplayName = input.DisplayName;
            commodityTree.IsLeaf = input.IsLeaf;

            await _commodityTreeManager.UpdateAsync(commodityTree);

            return await CreateCommodityDto(commodityTree);
        }

        [AbpAuthorize(AppPermissions.Pages_CommodityTrees_ManageCommodityTree)]
        public async Task<CommodityTreeDto> MoveCommodityTree(MoveCommodityTreeInput input)
        {
            await _commodityTreeManager.MoveAsync(input.Id, input.NewParentId);

            return await CreateCommodityDto(
                await _commodityTreeRepository.GetAsync(input.Id)
                );
        }

        [AbpAuthorize(AppPermissions.Pages_CommodityTrees_ManageCommodityTree)]
        public async Task DeleteCommodityTree(EntityDto<long> input)
        {
            await _commodityTreeManager.DeleteAsync(input.Id);
        }

        private async Task<CommodityTreeDto> CreateCommodityDto(CommodityTree commodityTree)
        {
            var dto = ObjectMapper.Map<CommodityTreeDto>(commodityTree);
            return dto;
        }
    }
}