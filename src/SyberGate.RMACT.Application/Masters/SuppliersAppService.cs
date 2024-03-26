

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
	[AbpAuthorize(AppPermissions.Pages_Administration_Suppliers)]
    public class SuppliersAppService : RMACTAppServiceBase, ISuppliersAppService
    {
		 private readonly IRepository<Supplier> _supplierRepository;
		 private readonly ISuppliersExcelExporter _suppliersExcelExporter;
		 

		  public SuppliersAppService(IRepository<Supplier> supplierRepository, ISuppliersExcelExporter suppliersExcelExporter ) 
		  {
			_supplierRepository = supplierRepository;
			_suppliersExcelExporter = suppliersExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetSupplierForViewDto>> GetAll(GetAllSuppliersInput input)
         {
			
			var filteredSuppliers = _supplierRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter);

			var pagedAndFilteredSuppliers = filteredSuppliers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var suppliers = from o in pagedAndFilteredSuppliers
                         select new GetSupplierForViewDto() {
							Supplier = new SupplierDto
							{
                                Code = o.Code,
                                Name = o.Name,
                                Id = o.Id
							}
						};

            var totalCount = await filteredSuppliers.CountAsync();

            return new PagedResultDto<GetSupplierForViewDto>(
                totalCount,
                await suppliers.ToListAsync()
            );
         }
		 
		 public async Task<GetSupplierForViewDto> GetSupplierForView(int id)
         {
            var supplier = await _supplierRepository.GetAsync(id);

            var output = new GetSupplierForViewDto { Supplier = ObjectMapper.Map<SupplierDto>(supplier) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_Suppliers_Edit)]
		 public async Task<GetSupplierForEditOutput> GetSupplierForEdit(EntityDto input)
         {
            var supplier = await _supplierRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetSupplierForEditOutput {Supplier = ObjectMapper.Map<CreateOrEditSupplierDto>(supplier)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditSupplierDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Suppliers_Create)]
		 protected virtual async Task Create(CreateOrEditSupplierDto input)
         {
            var supplier = ObjectMapper.Map<Supplier>(input);

			

            await _supplierRepository.InsertAsync(supplier);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Suppliers_Edit)]
		 protected virtual async Task Update(CreateOrEditSupplierDto input)
         {
            var supplier = await _supplierRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, supplier);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Suppliers_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _supplierRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetSuppliersToExcel(GetAllSuppliersForExcelInput input)
         {
			
			var filteredSuppliers = _supplierRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter);

			var query = (from o in filteredSuppliers
                         select new GetSupplierForViewDto() { 
							Supplier = new SupplierDto
							{
                                Code = o.Code,
                                Name = o.Name,
                                Id = o.Id
							}
						 });


            var supplierListDtos = await query.ToListAsync();

            return _suppliersExcelExporter.ExportToFile(supplierListDtos);
         }


    }
}