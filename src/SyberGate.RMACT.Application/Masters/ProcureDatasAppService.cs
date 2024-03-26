

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
	[AbpAuthorize(AppPermissions.Pages_Administration_ProcureDatas)]
    public class ProcureDatasAppService : RMACTAppServiceBase, IProcureDatasAppService
    {
		 private readonly IRepository<GlobusData> _procureDataRepository;
		 

		  public ProcureDatasAppService(IRepository<GlobusData> procureDataRepository ) 
		  {
			_procureDataRepository = procureDataRepository;
			
		  }

		 public async Task<PagedResultDto<GetProcureDataForViewDto>> GetAll(GetAllProcureDatasInput input)
         {
			
			var filteredProcureDatas = _procureDataRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PartNo.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.SupplierCode.Contains(input.Filter) || e.SuppliserName.Contains(input.Filter) || e.PriceCurrency.Contains(input.Filter) || e.Uom.Contains(input.Filter) || e.Buyer.Contains(input.Filter) || e.PlantCode.Contains(input.Filter) || e.PlantDescription.Contains(input.Filter) || e.ContractNo.Contains(input.Filter) || e.Status.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PartNoFilter),  e => e.PartNo == input.PartNoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description == input.DescriptionFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SupplierCodeFilter),  e => e.SupplierCode == input.SupplierCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SuppliserNameFilter),  e => e.SuppliserName == input.SuppliserNameFilter)
						.WhereIf(input.MinCurrentExwPriceFilter != null, e => e.CurrentExwPrice >= input.MinCurrentExwPriceFilter)
						.WhereIf(input.MaxCurrentExwPriceFilter != null, e => e.CurrentExwPrice <= input.MaxCurrentExwPriceFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceCurrencyFilter),  e => e.PriceCurrency == input.PriceCurrencyFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UomFilter),  e => e.Uom == input.UomFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BuyerFilter),  e => e.Buyer == input.BuyerFilter)
						.WhereIf(input.MinFromDateFilter != null, e => e.FromDate >= input.MinFromDateFilter)
						.WhereIf(input.MaxFromDateFilter != null, e => e.FromDate <= input.MaxFromDateFilter)
						.WhereIf(input.MinToDateFilter != null, e => e.ToDate >= input.MinToDateFilter)
						.WhereIf(input.MaxToDateFilter != null, e => e.ToDate <= input.MaxToDateFilter)
						.WhereIf(input.MinPackagingCostFilter != null, e => e.PackagingCost >= input.MinPackagingCostFilter)
						.WhereIf(input.MaxPackagingCostFilter != null, e => e.PackagingCost <= input.MaxPackagingCostFilter)
						.WhereIf(input.MinLogisticsCostFilter != null, e => e.LogisticsCost >= input.MinLogisticsCostFilter)
						.WhereIf(input.MaxLogisticsCostFilter != null, e => e.LogisticsCost <= input.MaxLogisticsCostFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PlantCodeFilter),  e => e.PlantCode == input.PlantCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PlantDescriptionFilter),  e => e.PlantDescription == input.PlantDescriptionFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContractNoFilter),  e => e.ContractNo == input.ContractNoFilter)
						.WhereIf(input.MinSOBFilter != null, e => e.SOB >= input.MinSOBFilter)
						.WhereIf(input.MaxSOBFilter != null, e => e.SOB <= input.MaxSOBFilter)
						.WhereIf(input.MinEPUFilter != null, e => e.EPU >= input.MinEPUFilter)
						.WhereIf(input.MaxEPUFilter != null, e => e.EPU <= input.MaxEPUFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter),  e => e.Status == input.StatusFilter)
						.WhereIf(input.MinCreationTimeFilter != null, e => e.CreationTime >= input.MinCreationTimeFilter)
						.WhereIf(input.MaxCreationTimeFilter != null, e => e.CreationTime <= input.MaxCreationTimeFilter)
						.WhereIf(input.MinDeletionTimeFilter != null, e => e.DeletionTime >= input.MinDeletionTimeFilter)
						.WhereIf(input.MaxDeletionTimeFilter != null, e => e.DeletionTime <= input.MaxDeletionTimeFilter)
						.WhereIf(input.IsDeletedFilter > -1,  e => (input.IsDeletedFilter == 1 && e.IsDeleted) || (input.IsDeletedFilter == 0 && !e.IsDeleted) )
						.WhereIf(input.MinLastModificationTimeFilter != null, e => e.LastModificationTime >= input.MinLastModificationTimeFilter)
						.WhereIf(input.MaxLastModificationTimeFilter != null, e => e.LastModificationTime <= input.MaxLastModificationTimeFilter);

			var pagedAndFilteredProcureDatas = filteredProcureDatas
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var procureDatas = from o in pagedAndFilteredProcureDatas
                         select new GetProcureDataForViewDto() {
							ProcureData = new ProcureDataDto
							{
                                PartNo = o.PartNo,
                                Description = o.Description,
                                SupplierCode = o.SupplierCode,
                                SuppliserName = o.SuppliserName,
                                CurrentExwPrice = o.CurrentExwPrice,
                                PriceCurrency = o.PriceCurrency,
                                Uom = o.Uom,
                                Buyer = o.Buyer,
                                FromDate = o.FromDate,
                                ToDate = o.ToDate,
                                PackagingCost = o.PackagingCost,
                                LogisticsCost = o.LogisticsCost,
                                PlantCode = o.PlantCode,
                                PlantDescription = o.PlantDescription,
                                ContractNo = o.ContractNo,
                                SOB = o.SOB,
                                EPU = o.EPU,
                                Status = o.Status,
                                CreationTime = o.CreationTime,
                                DeletionTime = o.DeletionTime,
                                IsDeleted = o.IsDeleted,
                                LastModificationTime = o.LastModificationTime,
                                Id = o.Id
							}
						};

            var totalCount = await filteredProcureDatas.CountAsync();

            return new PagedResultDto<GetProcureDataForViewDto>(
                totalCount,
                await procureDatas.ToListAsync()
            );
         }
		 
		 public async Task<GetProcureDataForViewDto> GetProcureDataForView(int id)
         {
            var procureData = await _procureDataRepository.GetAsync(id);

            var output = new GetProcureDataForViewDto { ProcureData = ObjectMapper.Map<ProcureDataDto>(procureData) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_ProcureDatas_Edit)]
		 public async Task<GetProcureDataForEditOutput> GetProcureDataForEdit(EntityDto input)
         {
            var procureData = await _procureDataRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetProcureDataForEditOutput {ProcureData = ObjectMapper.Map<CreateOrEditProcureDataDto>(procureData)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditProcureDataDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_ProcureDatas_Create)]
		 protected virtual async Task Create(CreateOrEditProcureDataDto input)
         {
            var procureData = ObjectMapper.Map<GlobusData>(input);

            await _procureDataRepository.InsertAsync(procureData);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_ProcureDatas_Edit)]
		 protected virtual async Task Update(CreateOrEditProcureDataDto input)
         {
            var procureData = await _procureDataRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, procureData);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_ProcureDatas_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _procureDataRepository.DeleteAsync(input.Id);
         } 
    }
}