

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using SyberGate.RMACT.Masters.Exporting;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace SyberGate.RMACT.Masters
{
	[AbpAuthorize(AppPermissions.Pages_Administration_UOMs)]
    public class UOMsAppService : RMACTAppServiceBase, IUOMsAppService
    {
		 private readonly IRepository<UOM> _uomRepository;
		 private readonly IUOMsExcelExporter _uoMsExcelExporter;
		 

		  public UOMsAppService(IRepository<UOM> uomRepository, IUOMsExcelExporter uoMsExcelExporter ) 
		  {
			_uomRepository = uomRepository;
			_uoMsExcelExporter = uoMsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetUOMForViewDto>> GetAll(GetAllUOMsInput input)
         {
			
			var filteredUOMs = _uomRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description == input.DescriptionFilter);

			var pagedAndFilteredUOMs = filteredUOMs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var uoMs = from o in pagedAndFilteredUOMs
                         select new GetUOMForViewDto() {
							UOM = new UOMDto
							{
                                Code = o.Code,
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredUOMs.CountAsync();

            return new PagedResultDto<GetUOMForViewDto>(
                totalCount,
                await uoMs.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_UOMs_Edit)]
		 public async Task<GetUOMForEditOutput> GetUOMForEdit(EntityDto input)
         {
            var uom = await _uomRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetUOMForEditOutput {UOM = ObjectMapper.Map<CreateOrEditUOMDto>(uom)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditUOMDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_UOMs_Create)]
		 protected virtual async Task Create(CreateOrEditUOMDto input)
         {
            var uom = ObjectMapper.Map<UOM>(input);

			

            await _uomRepository.InsertAsync(uom);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_UOMs_Edit)]
		 protected virtual async Task Update(CreateOrEditUOMDto input)
         {
            var uom = await _uomRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, uom);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_UOMs_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _uomRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetUOMsToExcel(GetAllUOMsForExcelInput input)
         {
			
			var filteredUOMs = _uomRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description == input.DescriptionFilter);

			var query = (from o in filteredUOMs
                         select new GetUOMForViewDto() { 
							UOM = new UOMDto
							{
                                Code = o.Code,
                                Description = o.Description,
                                Id = o.Id
							}
						 });


            var uomListDtos = await query.ToListAsync();

            return _uoMsExcelExporter.ExportToFile(uomListDtos);
         }


    }
}