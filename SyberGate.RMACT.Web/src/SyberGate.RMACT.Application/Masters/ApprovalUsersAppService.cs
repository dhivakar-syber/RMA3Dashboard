

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
using Abp.UI;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.Masters
{
	[AbpAuthorize(AppPermissions.Pages_Administration_ApprovalUsers)]
    public class ApprovalUsersAppService : RMACTAppServiceBase, IApprovalUsersAppService
    {
		 private readonly IRepository<ApprovalUser> _approvalUserRepository;
		 
         
		  public ApprovalUsersAppService(IRepository<ApprovalUser> approvalUserRepository  ) 
		  {
			_approvalUserRepository = approvalUserRepository;
			
			
		  }

		 public async Task<PagedResultDto<GetApprovalUserForViewDto>> GetAll(GetAllApprovalUsersInput input)
         {
			
			var filteredApprovalUsers = _approvalUserRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.UserName.Contains(input.Filter) || e.Department.Contains(input.Filter) || e.Email.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter),  e => e.UserName == input.UserNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentFilter),  e => e.Department == input.DepartmentFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter),  e => e.Email == input.EmailFilter);

			var pagedAndFilteredApprovalUsers = filteredApprovalUsers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);		
            
            var approvalUsers = from o in pagedAndFilteredApprovalUsers
                select new
                {
                  
                                o.UserName,
                                o.Department,
                                o.Email,
                  Id = o.Id                  
                };

            var totalCount = await filteredApprovalUsers.CountAsync();

            var dbList = await approvalUsers.ToListAsync();
            var results = new List<GetApprovalUserForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetApprovalUserForViewDto()
                {
                    ApprovalUser = new ApprovalUserDto
                    {
                       
                                UserName = o.UserName,
                                Department = o.Department,
                                Email = o.Email,
                        Id = o.Id,
                    }
                };
                
                results.Add(res);
            }

            return new PagedResultDto<GetApprovalUserForViewDto>(
                totalCount,
                results
            );
            
         }
		 
		 public async Task<GetApprovalUserForViewDto> GetApprovalUserForView(int id)
         {
            var approvalUser = await _approvalUserRepository.GetAsync(id);

            var output = new GetApprovalUserForViewDto { ApprovalUser = ObjectMapper.Map<ApprovalUserDto>(approvalUser) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_ApprovalUsers_Edit)]
		 public async Task<GetApprovalUserForEditOutput> GetApprovalUserForEdit(EntityDto input)
         {
            var approvalUser = await _approvalUserRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetApprovalUserForEditOutput {ApprovalUser = ObjectMapper.Map<CreateOrEditApprovalUserDto>(approvalUser)};
			
			
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditApprovalUserDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_ApprovalUsers_Create)]
		 protected virtual async Task Create(CreateOrEditApprovalUserDto input)
         {
            var approvalUser = ObjectMapper.Map<ApprovalUser>(input);

			

            await _approvalUserRepository.InsertAsync(approvalUser);
            
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_ApprovalUsers_Edit)]
		 protected virtual async Task Update(CreateOrEditApprovalUserDto input)
         {
            var approvalUser = await _approvalUserRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, approvalUser);
             
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_ApprovalUsers_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _approvalUserRepository.DeleteAsync(input.Id);
         } 
         
         
    }
}