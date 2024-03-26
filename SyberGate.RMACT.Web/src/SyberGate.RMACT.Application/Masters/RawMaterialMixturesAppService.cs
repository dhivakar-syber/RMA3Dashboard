using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters;

using SyberGate.RMACT.Configuration;
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
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Drawing;
using NPOI.SS.Formula.Functions;

namespace SyberGate.RMACT.Masters
{
	[AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures)]
    public class RawMaterialMixturesAppService : RMACTAppServiceBase, IRawMaterialMixturesAppService
    {
		 private readonly IRepository<RawMaterialMixture> _rawMaterialMixtureRepository;
		 private readonly IRepository<RawMaterialGrade, int> _lookup_rmGroupRepository;
		 private readonly IRepository<RawMaterialGrade,int> _lookup_rawMaterialGradeRepository;
		 private readonly IRepository<Buyer,int> _lookup_buyerRepository;
		 private readonly IRepository<Supplier,int> _lookup_supplierRepository;
		 

		  public RawMaterialMixturesAppService(IRepository<RawMaterialMixture> rawMaterialMixtureRepository , IRepository<RawMaterialGrade, int> lookup_rmGroupRepository, IRepository<RawMaterialGrade, int> lookup_rawMaterialGradeRepository, IRepository<Buyer, int> lookup_buyerRepository, IRepository<Supplier, int> lookup_supplierRepository) 
		  {
			_rawMaterialMixtureRepository = rawMaterialMixtureRepository;
			_lookup_rmGroupRepository = lookup_rmGroupRepository;
		_lookup_rawMaterialGradeRepository = lookup_rawMaterialGradeRepository;
		_lookup_buyerRepository = lookup_buyerRepository;
		_lookup_supplierRepository = lookup_supplierRepository;
		
		  }

		 public async Task<PagedResultDto<GetRawMaterialMixtureForViewDto>> GetAll(GetAllRawMaterialMixturesInput input)
         {
			
			var filteredRawMaterialMixtures = _rawMaterialMixtureRepository.GetAll()
						.Include( e => e.RMGroupFk)
						.Include( e => e.RawMaterialGradeFk)
						.Include( e => e.BuyerFk)
						.Include( e => e.SupplierFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.RMGroupFk.Name.Contains(input.Filter) || e.RawMaterialGradeFk.Name.Contains(input.Filter) || e.BuyerFk.Name.Contains(input.Filter) || e.SupplierFk.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RMGroupNameFilter), e => e.RMGroupFk != null && e.RMGroupFk.Name == input.RMGroupNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.RawMaterialGradeNameFilter), e => e.RawMaterialGradeFk != null && e.RawMaterialGradeFk.Name == input.RawMaterialGradeNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BuyerNameFilter), e => e.BuyerFk != null && e.BuyerFk.Name == input.BuyerNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SupplierNameFilter), e => e.SupplierFk != null && e.SupplierFk.Name == input.SupplierNameFilter);

			var pagedAndFilteredRawMaterialMixtures = filteredRawMaterialMixtures
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var rawMaterialMixtures = from o in pagedAndFilteredRawMaterialMixtures
                         join o1 in _lookup_rmGroupRepository.GetAll() on o.RMGroupId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_rawMaterialGradeRepository.GetAll() on o.RawMaterialGradeId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_buyerRepository.GetAll() on o.BuyerId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         join o4 in _lookup_supplierRepository.GetAll() on o.SupplierId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()
                         
                         select new GetRawMaterialMixtureForViewDto() {
							RawMaterialMixture = new RawMaterialMixtureDto
							{
                                WeightRatio = o.WeightRatio,
                                LossRatio = o.LossRatio,
                                Id = o.Id
							},
                         	RMGroupName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                         	RawMaterialGradeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                         	BuyerName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
                         	SupplierName = s4 == null || s4.Name == null ? "" : s4.Name.ToString()
						};

            var totalCount = await filteredRawMaterialMixtures.CountAsync();

            return new PagedResultDto<GetRawMaterialMixtureForViewDto>(
                totalCount,
                await rawMaterialMixtures.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures_Edit)]
		 public async Task<GetRawMaterialMixtureForEditOutput> GetRawMaterialMixtureForEdit(EntityDto input)
         {
            var rawMaterialMixture = await _rawMaterialMixtureRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRawMaterialMixtureForEditOutput {RawMaterialMixture = ObjectMapper.Map<CreateOrEditRawMaterialMixtureDto>(rawMaterialMixture)};

		    if (output.RawMaterialMixture.RMGroupId != null)
            {
                var _lookupRMGroup = await _lookup_rmGroupRepository.FirstOrDefaultAsync((int)output.RawMaterialMixture.RMGroupId);
                output.RMGroupName = _lookupRMGroup?.Name?.ToString();
            }

		    if (output.RawMaterialMixture.RawMaterialGradeId != null)
            {
                var _lookupRawMaterialGrade = await _lookup_rawMaterialGradeRepository.FirstOrDefaultAsync((int)output.RawMaterialMixture.RawMaterialGradeId);
                output.RawMaterialGradeName = _lookupRawMaterialGrade?.Name?.ToString();
            }

		    if (output.RawMaterialMixture.BuyerId != null)
            {
                var _lookupBuyer = await _lookup_buyerRepository.FirstOrDefaultAsync((int)output.RawMaterialMixture.BuyerId);
                output.BuyerName = _lookupBuyer?.Name?.ToString();
            }

		    if (output.RawMaterialMixture.SupplierId != null)
            {
                var _lookupSupplier = await _lookup_supplierRepository.FirstOrDefaultAsync((int)output.RawMaterialMixture.SupplierId);
                output.SupplierName = _lookupSupplier?.Name?.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRawMaterialMixtureDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

        public RawMaterialMixtureDto GetDefault()
        {
            RawMaterialMixtureDto rmMixture = new RawMaterialMixtureDto();

            var buyerId =SettingManager.GetSettingValueAsync(AppSettings.DashboardCustomization.Configuration + "." + "Buyer").Result;
            var supplierId = SettingManager.GetSettingValueAsync(AppSettings.DashboardCustomization.Configuration + "." + "Supplier").Result;
            var groupId = SettingManager.GetSettingValueAsync(AppSettings.DashboardCustomization.Configuration + "." + "Group").Result;
            
            if (!string.IsNullOrWhiteSpace(buyerId.ToString()) && Int32.Parse(buyerId.ToString()) > 0)
            {
                rmMixture.BuyerId = Int32.Parse(buyerId);
                rmMixture.BuyerName = _lookup_buyerRepository.GetAll().Where(w => w.Id.ToString() == buyerId).FirstOrDefault().Name;
            }
            if (!string.IsNullOrWhiteSpace(supplierId.ToString()) && Int32.Parse(supplierId.ToString()) > 0)
            {
                rmMixture.SupplierId = Int32.Parse(supplierId);
                rmMixture.SupplierName = _lookup_supplierRepository.GetAll().Where(w => w.Id.ToString() == supplierId).FirstOrDefault().Name;
            }
            if (!string.IsNullOrWhiteSpace(groupId.ToString()) && Int32.Parse(groupId.ToString()) > 0)
            {
                rmMixture.RMGroupId = Int32.Parse(groupId);
                rmMixture.RMGroupName = _lookup_rmGroupRepository.GetAll().Where(w => w.Id.ToString() == groupId).FirstOrDefault().Name;
            }

            return rmMixture;

        }

            [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures_Create)]
		 protected virtual async Task Create(CreateOrEditRawMaterialMixtureDto input)
         {
            var rawMaterialMixture = ObjectMapper.Map<RawMaterialMixture>(input);

			

            await _rawMaterialMixtureRepository.InsertAsync(rawMaterialMixture);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures_Edit)]
		 protected virtual async Task Update(CreateOrEditRawMaterialMixtureDto input)
         {
            var rawMaterialMixture = await _rawMaterialMixtureRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, rawMaterialMixture);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _rawMaterialMixtureRepository.DeleteAsync(input.Id);
         }
        
        [AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures)]
        public async Task<PagedResultDto<RawMaterialMixtureRMGroupLookupTableDto>> GetAllRMGroupForLookupTable(GetAllForLookupTableInput input)
         {
            var query = _lookup_rmGroupRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name != null && e.Name.Contains(input.Filter)
               ).Where(w => w.HasMixture == true) ;

            var totalCount = await query.CountAsync();

            var rmGroupList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RawMaterialMixtureRMGroupLookupTableDto>();
			foreach(var rmGroup in rmGroupList){
				lookupTableDtoList.Add(new RawMaterialMixtureRMGroupLookupTableDto
				{
					Id = rmGroup.Id,
					DisplayName = rmGroup.Name?.ToString()
				});
			}

            return new PagedResultDto<RawMaterialMixtureRMGroupLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures)]
         public async Task<PagedResultDto<RawMaterialMixtureRawMaterialGradeLookupTableDto>> GetAllRawMaterialGradeForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_rawMaterialGradeRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name != null && e.Name.Contains(input.Filter)
                ).Where(w=>w.IsGroup==false && w.HasMixture==false);

            var totalCount = await query.CountAsync();

            var rawMaterialGradeList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RawMaterialMixtureRawMaterialGradeLookupTableDto>();
			foreach(var rawMaterialGrade in rawMaterialGradeList){
				lookupTableDtoList.Add(new RawMaterialMixtureRawMaterialGradeLookupTableDto
				{
					Id = rawMaterialGrade.Id,
					DisplayName = rawMaterialGrade.Name?.ToString()
				});
			}

            return new PagedResultDto<RawMaterialMixtureRawMaterialGradeLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures)]
         public async Task<PagedResultDto<RawMaterialMixtureBuyerLookupTableDto>> GetAllBuyerForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_buyerRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name != null && e.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var buyerList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RawMaterialMixtureBuyerLookupTableDto>();
			foreach(var buyer in buyerList){
				lookupTableDtoList.Add(new RawMaterialMixtureBuyerLookupTableDto
				{
					Id = buyer.Id,
					DisplayName = buyer.Name?.ToString()
				});
			}

            return new PagedResultDto<RawMaterialMixtureBuyerLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures)]
         public async Task<PagedResultDto<RawMaterialMixtureSupplierLookupTableDto>> GetAllSupplierForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_supplierRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name != null && e.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var supplierList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RawMaterialMixtureSupplierLookupTableDto>();
			foreach(var supplier in supplierList){
				lookupTableDtoList.Add(new RawMaterialMixtureSupplierLookupTableDto
				{
					Id = supplier.Id,
					DisplayName = supplier.Name?.ToString()
				});
			}

            return new PagedResultDto<RawMaterialMixtureSupplierLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}