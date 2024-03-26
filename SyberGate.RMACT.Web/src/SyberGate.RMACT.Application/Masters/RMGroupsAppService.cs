

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
	[AbpAuthorize(AppPermissions.Pages_Administration_RMGroups)]
    public class RMGroupsAppService : RMACTAppServiceBase, IRMGroupsAppService
    {
		 private readonly IRepository<RMGroup> _rmGroupRepository;
		 

		  public RMGroupsAppService(IRepository<RMGroup> rmGroupRepository ) 
		  {
			_rmGroupRepository = rmGroupRepository;
			
		  }

		 public async Task<PagedResultDto<GetRMGroupForViewDto>> GetAll(GetAllRMGroupsInput input)
         {
			
			var filteredRMGroups = _rmGroupRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(input.HasMixtureFilter > -1,  e => (input.HasMixtureFilter == 1 && e.HasMixture) || (input.HasMixtureFilter == 0 && !e.HasMixture) );

			var pagedAndFilteredRMGroups = filteredRMGroups
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var rmGroups = from o in pagedAndFilteredRMGroups
                         select new GetRMGroupForViewDto() {
							RMGroup = new RMGroupDto
							{
                                Name = o.Name,
                                HasMixture = o.HasMixture,
                                Id = o.Id
							}
						};

            var totalCount = await filteredRMGroups.CountAsync();

            return new PagedResultDto<GetRMGroupForViewDto>(
                totalCount,
                await rmGroups.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_RMGroups_Edit)]
		 public async Task<GetRMGroupForEditOutput> GetRMGroupForEdit(EntityDto input)
         {
            var rmGroup = await _rmGroupRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRMGroupForEditOutput {RMGroup = ObjectMapper.Map<CreateOrEditRMGroupDto>(rmGroup)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRMGroupDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RMGroups_Create)]
		 protected virtual async Task Create(CreateOrEditRMGroupDto input)
         {
            var rmGroup = ObjectMapper.Map<RMGroup>(input);

			

            await _rmGroupRepository.InsertAsync(rmGroup);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RMGroups_Edit)]
		 protected virtual async Task Update(CreateOrEditRMGroupDto input)
         {
            var rmGroup = await _rmGroupRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, rmGroup);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RMGroups_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _rmGroupRepository.DeleteAsync(input.Id);
         } 
    }
}