

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
using SyberGate.RMACT.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace SyberGate.RMACT.Masters
{
	[AbpAuthorize(AppPermissions.Pages_Administration_Plants)]
    public class PlantsAppService : RMACTAppServiceBase, IPlantsAppService
    {
		 private readonly IRepository<Plant> _plantRepository;
		 

		  public PlantsAppService(IRepository<Plant> plantRepository ) 
		  {
			_plantRepository = plantRepository;
			
		  }

		 public async Task<PagedResultDto<GetPlantForViewDto>> GetAll(GetAllPlantsInput input)
         {
			
			var filteredPlants = _plantRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description == input.DescriptionFilter);

			var pagedAndFilteredPlants = filteredPlants
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var plants = from o in pagedAndFilteredPlants
                         select new GetPlantForViewDto() {
							Plant = new PlantDto
							{
                                Code = o.Code,
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPlants.CountAsync();

            return new PagedResultDto<GetPlantForViewDto>(
                totalCount,
                await plants.ToListAsync()
            );
         }
		 
		 public async Task<GetPlantForViewDto> GetPlantForView(int id)
         {
            var plant = await _plantRepository.GetAsync(id);

            var output = new GetPlantForViewDto { Plant = ObjectMapper.Map<PlantDto>(plant) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_Plants_Edit)]
		 public async Task<GetPlantForEditOutput> GetPlantForEdit(EntityDto input)
         {
            var plant = await _plantRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPlantForEditOutput {Plant = ObjectMapper.Map<CreateOrEditPlantDto>(plant)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPlantDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Plants_Create)]
		 protected virtual async Task Create(CreateOrEditPlantDto input)
         {
            var plant = ObjectMapper.Map<Plant>(input);

			

            await _plantRepository.InsertAsync(plant);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Plants_Edit)]
		 protected virtual async Task Update(CreateOrEditPlantDto input)
         {
            var plant = await _plantRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, plant);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Plants_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _plantRepository.DeleteAsync(input.Id);
         } 
    }
}