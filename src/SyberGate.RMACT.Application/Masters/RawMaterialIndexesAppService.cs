using SyberGate.RMACT.Masters;
					using System.Collections.Generic;
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
using SyberGate.RMACT.Masters.Dto;

namespace SyberGate.RMACT.Masters
{
	[AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes)]
    public class RawMaterialIndexesAppService : RMACTAppServiceBase, IRawMaterialIndexesAppService
    {
		 private readonly IRepository<RawMaterialIndex> _rawMaterialIndexRepository;
		 private readonly IRawMaterialIndexesExcelExporter _rawMaterialIndexesExcelExporter;
		 private readonly IRepository<IndexName,int> _lookup_indexNameRepository;
		 private readonly IRepository<Year,int> _lookup_yearRepository;
		 private readonly IRepository<RawMaterialGrade,int> _lookup_rawMaterialGradeRepository;
        private readonly IPartRepository _partRepository;

        public RawMaterialIndexesAppService(IRepository<RawMaterialIndex> rawMaterialIndexRepository, IRawMaterialIndexesExcelExporter rawMaterialIndexesExcelExporter , IRepository<IndexName, int> lookup_indexNameRepository, IRepository<Year, int> lookup_yearRepository, IRepository<RawMaterialGrade, int> lookup_rawMaterialGradeRepository, IPartRepository partRepository) 
		  {
			_rawMaterialIndexRepository = rawMaterialIndexRepository;
			_rawMaterialIndexesExcelExporter = rawMaterialIndexesExcelExporter;
			_lookup_indexNameRepository = lookup_indexNameRepository;
		_lookup_yearRepository = lookup_yearRepository;
		_lookup_rawMaterialGradeRepository = lookup_rawMaterialGradeRepository;
            _partRepository = partRepository;
        }

		 public async Task<PagedResultDto<GetRawMaterialIndexForViewDto>> GetAll(GetAllRawMaterialIndexesInput input)
         {
			var monthFilter = input.MonthFilter.HasValue
                        ? (Months) input.MonthFilter
                        : default;			
					
			var filteredRawMaterialIndexes = _rawMaterialIndexRepository.GetAll()
						.Include( e => e.IndexNameFk)
						.Include( e => e.YearFk)
						.Include( e => e.RawMaterialGradeFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MonthFilter.HasValue && input.MonthFilter > -1, e => e.Month == monthFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.IndexNameNameFilter), e => e.IndexNameFk != null && e.IndexNameFk.Name == input.IndexNameNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.YearNameFilter), e => e.YearFk != null && e.YearFk.Name == input.YearNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.RawMaterialGradeNameFilter), e => e.RawMaterialGradeFk != null && e.RawMaterialGradeFk.Name == input.RawMaterialGradeNameFilter);

			var pagedAndFilteredRawMaterialIndexes = filteredRawMaterialIndexes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var rawMaterialIndexes = from o in pagedAndFilteredRawMaterialIndexes
                         join o1 in _lookup_indexNameRepository.GetAll() on o.IndexNameId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_yearRepository.GetAll() on o.YearId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_rawMaterialGradeRepository.GetAll() on o.RawMaterialGradeId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         select new GetRawMaterialIndexForViewDto() {
							RawMaterialIndex = new RawMaterialIndexDto
							{
                                Month = o.Month,
                                Value = o.Value,
                                Id = o.Id
							},
                         	IndexNameName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                         	YearName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                         	RawMaterialGradeName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
						};

            var totalCount = await filteredRawMaterialIndexes.CountAsync();

            return new PagedResultDto<GetRawMaterialIndexForViewDto>(
                totalCount,
                await rawMaterialIndexes.ToListAsync()
            );
         }

         public async Task<string> GetAverageIndexValue(GetAllRawMaterialIndexesInput input)
        {
            var fromMonth = input.FromFilter != null ? input.FromFilter.ToString().Split("-")[0] : default;
            var fromYear = input.FromFilter != null  ? input.FromFilter.ToString().Split("-")[1] : default;

            var toMonth = input.ToFilter != null ? input.ToFilter.ToString().Split("-")[0] : default;
            var toYear = input.ToFilter != null ? input.ToFilter.ToString().Split("-")[1] : default;

            var filteredRawMaterialIndexes = _rawMaterialIndexRepository.GetAll()
                        .Include(e => e.IndexNameFk)
                        .Include(e => e.YearFk)
                        .Include(e => e.RawMaterialGradeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FromFilter), e => (int)e.Month >= Int32.Parse(fromMonth.ToString()) && (int)e.Month <= Int32.Parse(toMonth.ToString()))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ToFilter), e => e.YearId >= Int32.Parse(fromYear.ToString()) && e.YearId <= Int32.Parse(toYear.ToString()))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IndexNameNameFilter), e => e.IndexNameFk != null && e.IndexNameFk.Name == input.IndexNameNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RawMaterialGradeNameFilter), e => e.RawMaterialGradeFk != null && e.RawMaterialGradeFk.Name == input.RawMaterialGradeNameFilter);

            var output = filteredRawMaterialIndexes.Average(a => a.Value).ToString();

            return output;
        }
		 
		 public async Task<GetRawMaterialIndexForViewDto> GetRawMaterialIndexForView(int id)
         {
            var rawMaterialIndex = await _rawMaterialIndexRepository.GetAsync(id);

            var output = new GetRawMaterialIndexForViewDto { RawMaterialIndex = ObjectMapper.Map<RawMaterialIndexDto>(rawMaterialIndex) };

		    if (output.RawMaterialIndex.IndexNameId != null)
            {
                var _lookupIndexName = await _lookup_indexNameRepository.FirstOrDefaultAsync((int)output.RawMaterialIndex.IndexNameId);
                output.IndexNameName = _lookupIndexName?.Name?.ToString();
            }

		    if (output.RawMaterialIndex.YearId != null)
            {
                var _lookupYear = await _lookup_yearRepository.FirstOrDefaultAsync((int)output.RawMaterialIndex.YearId);
                output.YearName = _lookupYear?.Name?.ToString();
            }

		    if (output.RawMaterialIndex.RawMaterialGradeId != null)
            {
                var _lookupRawMaterialGrade = await _lookup_rawMaterialGradeRepository.FirstOrDefaultAsync((int)output.RawMaterialIndex.RawMaterialGradeId);
                output.RawMaterialGradeName = _lookupRawMaterialGrade?.Name?.ToString();
            }
			
            return output;
         }
		
        public PagedResultDto<GetRMIndexPrice> GetRMPriceIndexList(GetAllRawMaterialIndexesInput input)
        {
            var spInput = new GetRMIndexPriceSP{ IndexName = input.IndexNameNameFilter, RMGrade = input.RawMaterialGradeNameFilter, Year = input.YearNameFilter };
            var rmIndexes = _partRepository.GetRMPriceIndexList(spInput);
            List<GetRMIndexPrice> rmPrice = new List<GetRMIndexPrice>();

            foreach (var item in rmIndexes.Result)
            {
                rmPrice.Add(new GetRMIndexPrice { 
                    RMGrade = item.RMGrade,
                    Year = item.Year,
                    IndexName = item.IndexName,
                    Jan = item.Jan,
                    Feb = item.Feb,
                    Mar = item.Mar,
                    Apr = item.Apr,
                    May = item.May,
                    Jun = item.Jun,
                    Jul = item.Jul,
                    Aug = item.Aug,
                    Sep = item.Sep,
                    Oct = item.Oct,
                    Nov = item.Nov,
                    Dec = item.Dec
                });
            }
            var count = rmPrice.Count();
           

            return new PagedResultDto<GetRMIndexPrice>(count, rmPrice.ToList());
        }


		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes_Edit)]
		 public async Task<GetRawMaterialIndexForEditOutput> GetRawMaterialIndexForEdit(EntityDto input)
         {
            var rawMaterialIndex = await _rawMaterialIndexRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRawMaterialIndexForEditOutput {RawMaterialIndex = ObjectMapper.Map<CreateOrEditRawMaterialIndexDto>(rawMaterialIndex)};

		    if (output.RawMaterialIndex.IndexNameId != null)
            {
                var _lookupIndexName = await _lookup_indexNameRepository.FirstOrDefaultAsync((int)output.RawMaterialIndex.IndexNameId);
                output.IndexNameName = _lookupIndexName?.Name?.ToString();
            }

		    if (output.RawMaterialIndex.YearId != null)
            {
                var _lookupYear = await _lookup_yearRepository.FirstOrDefaultAsync((int)output.RawMaterialIndex.YearId);
                output.YearName = _lookupYear?.Name?.ToString();
            }

		    if (output.RawMaterialIndex.RawMaterialGradeId != null)
            {
                var _lookupRawMaterialGrade = await _lookup_rawMaterialGradeRepository.FirstOrDefaultAsync((int)output.RawMaterialIndex.RawMaterialGradeId);
                output.RawMaterialGradeName = _lookupRawMaterialGrade?.Name?.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRawMaterialIndexDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes_Create)]
		 protected virtual async Task Create(CreateOrEditRawMaterialIndexDto input)
         {
            var rawMaterialIndex = ObjectMapper.Map<RawMaterialIndex>(input);

			

            await _rawMaterialIndexRepository.InsertAsync(rawMaterialIndex);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes_Edit)]
		 protected virtual async Task Update(CreateOrEditRawMaterialIndexDto input)
         {
            var rawMaterialIndex = await _rawMaterialIndexRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, rawMaterialIndex);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _rawMaterialIndexRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetRawMaterialIndexesToExcel(GetAllRawMaterialIndexesForExcelInput input)
         {
			var monthFilter = input.MonthFilter.HasValue
                        ? (Months) input.MonthFilter
                        : default;			
					
			var filteredRawMaterialIndexes = _rawMaterialIndexRepository.GetAll()
						.Include( e => e.IndexNameFk)
						.Include( e => e.YearFk)
						.Include( e => e.RawMaterialGradeFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MonthFilter.HasValue && input.MonthFilter > -1, e => e.Month == monthFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.IndexNameNameFilter), e => e.IndexNameFk != null && e.IndexNameFk.Name == input.IndexNameNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.YearNameFilter), e => e.YearFk != null && e.YearFk.Name == input.YearNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.RawMaterialGradeNameFilter), e => e.RawMaterialGradeFk != null && e.RawMaterialGradeFk.Name == input.RawMaterialGradeNameFilter);

			var query = (from o in filteredRawMaterialIndexes
                         join o1 in _lookup_indexNameRepository.GetAll() on o.IndexNameId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_yearRepository.GetAll() on o.YearId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_rawMaterialGradeRepository.GetAll() on o.RawMaterialGradeId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         select new GetRawMaterialIndexForViewDto() { 
							RawMaterialIndex = new RawMaterialIndexDto
							{
                                Month = o.Month,
                                Value = o.Value,
                                Id = o.Id
							},
                         	IndexNameName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                         	YearName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                         	RawMaterialGradeName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
						 });


            var rawMaterialIndexListDtos = await query.ToListAsync();

            return _rawMaterialIndexesExcelExporter.ExportToFile(rawMaterialIndexListDtos);
         }


			[AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes)]
			public async Task<List<RawMaterialIndexIndexNameLookupTableDto>> GetAllIndexNameForTableDropdown()
			{
				return await _lookup_indexNameRepository.GetAll()
					.Select(indexName => new RawMaterialIndexIndexNameLookupTableDto
					{
						Id = indexName.Id,
						DisplayName = indexName == null || indexName.Name == null ? "" : indexName.Name.ToString()
					}).ToListAsync();
			}
							
			[AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes)]
			public async Task<List<RawMaterialIndexYearLookupTableDto>> GetAllYearForTableDropdown()
			{
				return await _lookup_yearRepository.GetAll()
					.Select(year => new RawMaterialIndexYearLookupTableDto
					{
						Id = year.Id,
						DisplayName = year == null || year.Name == null ? "" : year.Name.ToString()
					}).ToListAsync();
			}
							

		[AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes)]
         public async Task<PagedResultDto<RawMaterialIndexRawMaterialGradeLookupTableDto>> GetAllRawMaterialGradeForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_rawMaterialGradeRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name != null && e.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var rawMaterialGradeList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RawMaterialIndexRawMaterialGradeLookupTableDto>();
			foreach(var rawMaterialGrade in rawMaterialGradeList){
				lookupTableDtoList.Add(new RawMaterialIndexRawMaterialGradeLookupTableDto
				{
					Id = rawMaterialGrade.Id,
					DisplayName = rawMaterialGrade.Name?.ToString()
				});
			}

            return new PagedResultDto<RawMaterialIndexRawMaterialGradeLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}