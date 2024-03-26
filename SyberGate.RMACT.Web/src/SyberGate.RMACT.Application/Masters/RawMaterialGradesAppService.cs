using SyberGate.RMACT.Masters;


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
	[AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialGrades)]
    public class RawMaterialGradesAppService : RMACTAppServiceBase, IRawMaterialGradesAppService
    {
		 private readonly IRepository<RawMaterialGrade> _rawMaterialGradeRepository;
		 private readonly IRawMaterialGradesExcelExporter _rawMaterialGradesExcelExporter;
		 private readonly IRepository<RawMaterialGrade,int> _lookup_rawMaterialGradeRepository;
		 

		  public RawMaterialGradesAppService(IRepository<RawMaterialGrade> rawMaterialGradeRepository, IRawMaterialGradesExcelExporter rawMaterialGradesExcelExporter , IRepository<RawMaterialGrade, int> lookup_rawMaterialGradeRepository) 
		  {
			_rawMaterialGradeRepository = rawMaterialGradeRepository;
			_rawMaterialGradesExcelExporter = rawMaterialGradesExcelExporter;
			_lookup_rawMaterialGradeRepository = lookup_rawMaterialGradeRepository;
		
		  }

		 public async Task<PagedResultDto<GetRawMaterialGradeForViewDto>> GetAll(GetAllRawMaterialGradesInput input)
         {
			
			var filteredRawMaterialGrades = _rawMaterialGradeRepository.GetAll()
						.Include( e => e.RawMaterialGradeFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(input.IsGroupFilter > -1,  e => (input.IsGroupFilter == 1 && e.IsGroup) || (input.IsGroupFilter == 0 && !e.IsGroup) )
						.WhereIf(input.HasMixtureFilter > -1,  e => (input.HasMixtureFilter == 1 && e.HasMixture) || (input.HasMixtureFilter == 0 && !e.HasMixture) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.RawMaterialGradeNameFilter), e => e.RawMaterialGradeFk != null && e.RawMaterialGradeFk.Name == input.RawMaterialGradeNameFilter);

			var pagedAndFilteredRawMaterialGrades = filteredRawMaterialGrades
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var rawMaterialGrades = from o in pagedAndFilteredRawMaterialGrades
                         join o1 in _lookup_rawMaterialGradeRepository.GetAll() on o.RawMaterialGradeId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRawMaterialGradeForViewDto() {
							RawMaterialGrade = new RawMaterialGradeDto
							{
                                Name = o.Name,
                                IsGroup = o.IsGroup,
                                HasMixture = o.HasMixture,
                                Id = o.Id
							},
                         	RawMaterialGradeName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
						};

            var totalCount = await filteredRawMaterialGrades.CountAsync();

            return new PagedResultDto<GetRawMaterialGradeForViewDto>(
                totalCount,
                await rawMaterialGrades.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialGrades_Edit)]
		 public async Task<GetRawMaterialGradeForEditOutput> GetRawMaterialGradeForEdit(EntityDto input)
         {
            var rawMaterialGrade = await _rawMaterialGradeRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRawMaterialGradeForEditOutput {RawMaterialGrade = ObjectMapper.Map<CreateOrEditRawMaterialGradeDto>(rawMaterialGrade)};

		    if (output.RawMaterialGrade.RawMaterialGradeId != null)
            {
                var _lookupRawMaterialGrade = await _lookup_rawMaterialGradeRepository.FirstOrDefaultAsync((int)output.RawMaterialGrade.RawMaterialGradeId);
                output.RawMaterialGradeName = _lookupRawMaterialGrade?.Name?.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRawMaterialGradeDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialGrades_Create)]
		 protected virtual async Task Create(CreateOrEditRawMaterialGradeDto input)
         {
            var rawMaterialGrade = ObjectMapper.Map<RawMaterialGrade>(input);

			

            await _rawMaterialGradeRepository.InsertAsync(rawMaterialGrade);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialGrades_Edit)]
		 protected virtual async Task Update(CreateOrEditRawMaterialGradeDto input)
         {
            var rawMaterialGrade = await _rawMaterialGradeRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, rawMaterialGrade);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialGrades_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _rawMaterialGradeRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetRawMaterialGradesToExcel(GetAllRawMaterialGradesForExcelInput input)
         {
			
			var filteredRawMaterialGrades = _rawMaterialGradeRepository.GetAll()
						.Include( e => e.RawMaterialGradeFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(input.IsGroupFilter > -1,  e => (input.IsGroupFilter == 1 && e.IsGroup) || (input.IsGroupFilter == 0 && !e.IsGroup) )
						.WhereIf(input.HasMixtureFilter > -1,  e => (input.HasMixtureFilter == 1 && e.HasMixture) || (input.HasMixtureFilter == 0 && !e.HasMixture) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.RawMaterialGradeNameFilter), e => e.RawMaterialGradeFk != null && e.RawMaterialGradeFk.Name == input.RawMaterialGradeNameFilter);

			var query = (from o in filteredRawMaterialGrades
                         join o1 in _lookup_rawMaterialGradeRepository.GetAll() on o.RawMaterialGradeId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRawMaterialGradeForViewDto() { 
							RawMaterialGrade = new RawMaterialGradeDto
							{
                                Name = o.Name,
                                IsGroup = o.IsGroup,
                                HasMixture = o.HasMixture,
                                Id = o.Id
							},
                         	RawMaterialGradeName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
						 });


            var rawMaterialGradeListDtos = await query.ToListAsync();

            return _rawMaterialGradesExcelExporter.ExportToFile(rawMaterialGradeListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialGrades)]
         public async Task<PagedResultDto<RawMaterialGradeRawMaterialGradeLookupTableDto>> GetAllRawMaterialGradeForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_rawMaterialGradeRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name != null && e.Name.Contains(input.Filter) && e.IsGroup == true
                ).Where(w=>w.IsGroup==true);

            var totalCount = await query.CountAsync();

            var rawMaterialGradeList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RawMaterialGradeRawMaterialGradeLookupTableDto>();
			foreach(var rawMaterialGrade in rawMaterialGradeList){
				lookupTableDtoList.Add(new RawMaterialGradeRawMaterialGradeLookupTableDto
				{
					Id = rawMaterialGrade.Id,
					DisplayName = rawMaterialGrade.Name?.ToString()
				});
			}

            return new PagedResultDto<RawMaterialGradeRawMaterialGradeLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}