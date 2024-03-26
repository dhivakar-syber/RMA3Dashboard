using SyberGate.RMACT.Authorization.Users;
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
	[AbpAuthorize(AppPermissions.Pages_Administration_Buyers)]
    public class BuyersAppService : RMACTAppServiceBase, IBuyersAppService
    {
		 private readonly IRepository<Buyer> _buyerRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public BuyersAppService(IRepository<Buyer> buyerRepository , IRepository<User, long> lookup_userRepository) 
		  {
			_buyerRepository = buyerRepository;
			_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetBuyerForViewDto>> GetAll(GetAllBuyersInput input)
         {
			
			var filteredBuyers = _buyerRepository.GetAll()
						.Include( e => e.UserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserFk != null && e.UserFk.Name == input.UserNameFilter);

			var pagedAndFilteredBuyers = filteredBuyers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var buyers = from o in pagedAndFilteredBuyers
                         join o1 in _lookup_userRepository.GetAll() on o.UserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetBuyerForViewDto() {
							Buyer = new BuyerDto
							{
                                Code = o.Code,
                                Name = o.Name,
                                Id = o.Id,
								Department = o.Department

							},
                         	UserName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
						};

            var totalCount = await filteredBuyers.CountAsync();

            return new PagedResultDto<GetBuyerForViewDto>(
                totalCount,
                await buyers.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_Buyers_Edit)]
		 public async Task<GetBuyerForEditOutput> GetBuyerForEdit(EntityDto input)
         {
            var buyer = await _buyerRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetBuyerForEditOutput {Buyer = ObjectMapper.Map<CreateOrEditBuyerDto>(buyer)};

		    if (output.Buyer.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.Buyer.UserId);
                output.UserName = _lookupUser?.Name?.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditBuyerDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Buyers_Create)]
		 protected virtual async Task Create(CreateOrEditBuyerDto input)
         {
            var buyer = ObjectMapper.Map<Buyer>(input);

			

            await _buyerRepository.InsertAsync(buyer);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Buyers_Edit)]
		 protected virtual async Task Update(CreateOrEditBuyerDto input)
         {
            var buyer = await _buyerRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, buyer);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Buyers_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _buyerRepository.DeleteAsync(input.Id);
         } 
			[AbpAuthorize(AppPermissions.Pages_Administration_Buyers)]
			public async Task<List<BuyerUserLookupTableDto>> GetAllUserForTableDropdown()
			{
				return await _lookup_userRepository.GetAll()
					.Select(user => new BuyerUserLookupTableDto
					{
						Id = user.Id,
						DisplayName = user == null || user.Name == null ? "" : user.Name.ToString()
					}).ToListAsync();
			}
							
    }
}