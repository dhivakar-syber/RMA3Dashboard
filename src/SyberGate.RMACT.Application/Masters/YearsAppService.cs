

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
	[AbpAuthorize(AppPermissions.Pages_Administration_Years)]
    public class YearsAppService : RMACTAppServiceBase, IYearsAppService
    {
		 private readonly IRepository<Year> _yearRepository;
		 

		  public YearsAppService(IRepository<Year> yearRepository ) 
		  {
			_yearRepository = yearRepository;
			
		  }

		 public async Task<PagedResultDto<GetYearForViewDto>> GetAll(GetAllYearsInput input)
         {
			
			var filteredYears = _yearRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter));

			var pagedAndFilteredYears = filteredYears
                .OrderBy(input.Sorting ?? "seqno desc")
                .PageBy(input);

			var years = from o in pagedAndFilteredYears
                         select new GetYearForViewDto() {
							Year = new YearDto
							{
                                Name = o.Name,
                                Id = o.Id,
                                SeqNo = o.SeqNo
							}
						};

            var totalCount = await filteredYears.CountAsync();

            return new PagedResultDto<GetYearForViewDto>(
                totalCount,
                await years.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_Years_Edit)]
		 public async Task<GetYearForEditOutput> GetYearForEdit(EntityDto input)
         {
            var year = await _yearRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetYearForEditOutput {Year = ObjectMapper.Map<CreateOrEditYearDto>(year)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditYearDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Years_Create)]
		 protected virtual async Task Create(CreateOrEditYearDto input)
         {
            var year = ObjectMapper.Map<Year>(input);

			

            await _yearRepository.InsertAsync(year);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Years_Edit)]
		 protected virtual async Task Update(CreateOrEditYearDto input)
         {
            var year = await _yearRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, year);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Years_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _yearRepository.DeleteAsync(input.Id);
         } 
    }
}