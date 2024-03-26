using SyberGate.RMACT.Masters;
using System.Collections.Generic;
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
	[AbpAuthorize(AppPermissions.Pages_Administration_PartModelMatrixes)]
    public class PartModelMatrixesAppService : RMACTAppServiceBase, IPartModelMatrixesAppService
    {
		 private readonly IRepository<PartModelMatrix> _partModelMatrixRepository;
		 private readonly IRepository<LeadModel,int> _lookup_leadModelRepository;
		 

		  public PartModelMatrixesAppService(IRepository<PartModelMatrix> partModelMatrixRepository , IRepository<LeadModel, int> lookup_leadModelRepository) 
		  {
			_partModelMatrixRepository = partModelMatrixRepository;
			_lookup_leadModelRepository = lookup_leadModelRepository;
		
		  }

		 public async Task<PagedResultDto<GetPartModelMatrixForViewDto>> GetAll(GetAllPartModelMatrixesInput input)
         {
			
			var filteredPartModelMatrixes = _partModelMatrixRepository.GetAll()
						.Include( e => e.LeadModelFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PartNumber.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PartNumberFilter),  e => e.PartNumber == input.PartNumberFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.LeadModelNameFilter), e => e.LeadModelFk != null && e.LeadModelFk.Name == input.LeadModelNameFilter);

			var pagedAndFilteredPartModelMatrixes = filteredPartModelMatrixes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var partModelMatrixes = from o in pagedAndFilteredPartModelMatrixes
                         join o1 in _lookup_leadModelRepository.GetAll() on o.LeadModelId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetPartModelMatrixForViewDto() {
							PartModelMatrix = new PartModelMatrixDto
							{
                                PartNumber = o.PartNumber,
                                Quantity = o.Quantity,
                                Id = o.Id
							},
                         	LeadModelName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
						};

            var totalCount = await filteredPartModelMatrixes.CountAsync();

            return new PagedResultDto<GetPartModelMatrixForViewDto>(
                totalCount,
                await partModelMatrixes.ToListAsync()
            );
         }
		 
		 public async Task<GetPartModelMatrixForViewDto> GetPartModelMatrixForView(int id)
         {
            var partModelMatrix = await _partModelMatrixRepository.GetAsync(id);

            var output = new GetPartModelMatrixForViewDto { PartModelMatrix = ObjectMapper.Map<PartModelMatrixDto>(partModelMatrix) };

		    if (output.PartModelMatrix.LeadModelId != null)
            {
                var _lookupLeadModel = await _lookup_leadModelRepository.FirstOrDefaultAsync((int)output.PartModelMatrix.LeadModelId);
                output.LeadModelName = _lookupLeadModel?.Name?.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_PartModelMatrixes_Edit)]
		 public async Task<GetPartModelMatrixForEditOutput> GetPartModelMatrixForEdit(EntityDto input)
         {
            var partModelMatrix = await _partModelMatrixRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPartModelMatrixForEditOutput {PartModelMatrix = ObjectMapper.Map<CreateOrEditPartModelMatrixDto>(partModelMatrix)};

		    if (output.PartModelMatrix.LeadModelId != null)
            {
                var _lookupLeadModel = await _lookup_leadModelRepository.FirstOrDefaultAsync((int)output.PartModelMatrix.LeadModelId);
                output.LeadModelName = _lookupLeadModel?.Name?.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPartModelMatrixDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PartModelMatrixes_Create)]
		 protected virtual async Task Create(CreateOrEditPartModelMatrixDto input)
         {
            var partModelMatrix = ObjectMapper.Map<PartModelMatrix>(input);

			

            await _partModelMatrixRepository.InsertAsync(partModelMatrix);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PartModelMatrixes_Edit)]
		 protected virtual async Task Update(CreateOrEditPartModelMatrixDto input)
         {
            var partModelMatrix = await _partModelMatrixRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, partModelMatrix);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PartModelMatrixes_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _partModelMatrixRepository.DeleteAsync(input.Id);
         } 
			[AbpAuthorize(AppPermissions.Pages_Administration_PartModelMatrixes)]
			public async Task<List<PartModelMatrixLeadModelLookupTableDto>> GetAllLeadModelForTableDropdown()
			{
				return await _lookup_leadModelRepository.GetAll()
					.Select(leadModel => new PartModelMatrixLeadModelLookupTableDto
					{
						Id = leadModel.Id,
						DisplayName = leadModel == null || leadModel.Name == null ? "" : leadModel.Name.ToString()
					}).ToListAsync();
			}
							
    }
}