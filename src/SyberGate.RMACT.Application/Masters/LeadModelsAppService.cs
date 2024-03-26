

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
	[AbpAuthorize(AppPermissions.Pages_Administration_LeadModels)]
    public class LeadModelsAppService : RMACTAppServiceBase, ILeadModelsAppService
    {
		 private readonly IRepository<LeadModel> _leadModelRepository;
		 

		  public LeadModelsAppService(IRepository<LeadModel> leadModelRepository ) 
		  {
			_leadModelRepository = leadModelRepository;
			
		  }

		 public async Task<PagedResultDto<GetLeadModelForViewDto>> GetAll(GetAllLeadModelsInput input)
         {
			
			var filteredLeadModels = _leadModelRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description == input.DescriptionFilter);

			var pagedAndFilteredLeadModels = filteredLeadModels
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var leadModels = from o in pagedAndFilteredLeadModels
                         select new GetLeadModelForViewDto() {
							LeadModel = new LeadModelDto
							{
                                Name = o.Name,
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredLeadModels.CountAsync();

            return new PagedResultDto<GetLeadModelForViewDto>(
                totalCount,
                await leadModels.ToListAsync()
            );
         }
		 
		 public async Task<GetLeadModelForViewDto> GetLeadModelForView(int id)
         {
            var leadModel = await _leadModelRepository.GetAsync(id);

            var output = new GetLeadModelForViewDto { LeadModel = ObjectMapper.Map<LeadModelDto>(leadModel) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_LeadModels_Edit)]
		 public async Task<GetLeadModelForEditOutput> GetLeadModelForEdit(EntityDto input)
         {
            var leadModel = await _leadModelRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLeadModelForEditOutput {LeadModel = ObjectMapper.Map<CreateOrEditLeadModelDto>(leadModel)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLeadModelDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_LeadModels_Create)]
		 protected virtual async Task Create(CreateOrEditLeadModelDto input)
         {
            var leadModel = ObjectMapper.Map<LeadModel>(input);

			

            await _leadModelRepository.InsertAsync(leadModel);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_LeadModels_Edit)]
		 protected virtual async Task Update(CreateOrEditLeadModelDto input)
         {
            var leadModel = await _leadModelRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, leadModel);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_LeadModels_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _leadModelRepository.DeleteAsync(input.Id);
         } 
    }
}