

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
	[AbpAuthorize(AppPermissions.Pages_Administration_IndexNames)]
    public class IndexNamesAppService : RMACTAppServiceBase, IIndexNamesAppService
    {
		 private readonly IRepository<IndexName> _indexNameRepository;
		 private readonly IIndexNamesExcelExporter _indexNamesExcelExporter;
		 

		  public IndexNamesAppService(IRepository<IndexName> indexNameRepository, IIndexNamesExcelExporter indexNamesExcelExporter ) 
		  {
			_indexNameRepository = indexNameRepository;
			_indexNamesExcelExporter = indexNamesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetIndexNameForViewDto>> GetAll(GetAllIndexNamesInput input)
         {
			
			var filteredIndexNames = _indexNameRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter);

			var pagedAndFilteredIndexNames = filteredIndexNames
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var indexNames = from o in pagedAndFilteredIndexNames
                         select new GetIndexNameForViewDto() {
							IndexName = new IndexNameDto
							{
                                Name = o.Name,
                                Id = o.Id
							}
						};

            var totalCount = await filteredIndexNames.CountAsync();

            return new PagedResultDto<GetIndexNameForViewDto>(
                totalCount,
                await indexNames.ToListAsync()
            );
         }
		 
		 public async Task<GetIndexNameForViewDto> GetIndexNameForView(int id)
         {
            var indexName = await _indexNameRepository.GetAsync(id);

            var output = new GetIndexNameForViewDto { IndexName = ObjectMapper.Map<IndexNameDto>(indexName) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_IndexNames_Edit)]
		 public async Task<GetIndexNameForEditOutput> GetIndexNameForEdit(EntityDto input)
         {
            var indexName = await _indexNameRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetIndexNameForEditOutput {IndexName = ObjectMapper.Map<CreateOrEditIndexNameDto>(indexName)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditIndexNameDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_IndexNames_Create)]
		 protected virtual async Task Create(CreateOrEditIndexNameDto input)
         {
            var indexName = ObjectMapper.Map<IndexName>(input);

			

            await _indexNameRepository.InsertAsync(indexName);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_IndexNames_Edit)]
		 protected virtual async Task Update(CreateOrEditIndexNameDto input)
         {
            var indexName = await _indexNameRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, indexName);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_IndexNames_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _indexNameRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetIndexNamesToExcel(GetAllIndexNamesForExcelInput input)
         {
			
			var filteredIndexNames = _indexNameRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter);

			var query = (from o in filteredIndexNames
                         select new GetIndexNameForViewDto() { 
							IndexName = new IndexNameDto
							{
                                Name = o.Name,
                                Id = o.Id
							}
						 });


            var indexNameListDtos = await query.ToListAsync();

            return _indexNamesExcelExporter.ExportToFile(indexNameListDtos);
         }


    }
}