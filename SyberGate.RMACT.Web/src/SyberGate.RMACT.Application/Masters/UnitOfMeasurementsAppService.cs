

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
	[AbpAuthorize(AppPermissions.Pages_Administration_UnitOfMeasurements)]
    public class UnitOfMeasurementsAppService : RMACTAppServiceBase, IUnitOfMeasurementsAppService
    {
		 private readonly IRepository<UnitOfMeasurement> _unitOfMeasurementRepository;
		 private readonly IUnitOfMeasurementsExcelExporter _unitOfMeasurementsExcelExporter;
		 

		  public UnitOfMeasurementsAppService(IRepository<UnitOfMeasurement> unitOfMeasurementRepository, IUnitOfMeasurementsExcelExporter unitOfMeasurementsExcelExporter ) 
		  {
			_unitOfMeasurementRepository = unitOfMeasurementRepository;
			_unitOfMeasurementsExcelExporter = unitOfMeasurementsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetUnitOfMeasurementForViewDto>> GetAll(GetAllUnitOfMeasurementsInput input)
         {
			
			var filteredUnitOfMeasurements = _unitOfMeasurementRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter);

			var pagedAndFilteredUnitOfMeasurements = filteredUnitOfMeasurements
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var unitOfMeasurements = from o in pagedAndFilteredUnitOfMeasurements
                         select new GetUnitOfMeasurementForViewDto() {
							UnitOfMeasurement = new UnitOfMeasurementDto
							{
                                Code = o.Code,
                                Name = o.Name,
                                Id = o.Id
							}
						};

            var totalCount = await filteredUnitOfMeasurements.CountAsync();

            return new PagedResultDto<GetUnitOfMeasurementForViewDto>(
                totalCount,
                await unitOfMeasurements.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_UnitOfMeasurements_Edit)]
		 public async Task<GetUnitOfMeasurementForEditOutput> GetUnitOfMeasurementForEdit(EntityDto input)
         {
            var unitOfMeasurement = await _unitOfMeasurementRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetUnitOfMeasurementForEditOutput {UnitOfMeasurement = ObjectMapper.Map<CreateOrEditUnitOfMeasurementDto>(unitOfMeasurement)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditUnitOfMeasurementDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_UnitOfMeasurements_Create)]
		 protected virtual async Task Create(CreateOrEditUnitOfMeasurementDto input)
         {
            var unitOfMeasurement = ObjectMapper.Map<UnitOfMeasurement>(input);

			

            await _unitOfMeasurementRepository.InsertAsync(unitOfMeasurement);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_UnitOfMeasurements_Edit)]
		 protected virtual async Task Update(CreateOrEditUnitOfMeasurementDto input)
         {
            var unitOfMeasurement = await _unitOfMeasurementRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, unitOfMeasurement);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_UnitOfMeasurements_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _unitOfMeasurementRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetUnitOfMeasurementsToExcel(GetAllUnitOfMeasurementsForExcelInput input)
         {
			
			var filteredUnitOfMeasurements = _unitOfMeasurementRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter);

			var query = (from o in filteredUnitOfMeasurements
                         select new GetUnitOfMeasurementForViewDto() { 
							UnitOfMeasurement = new UnitOfMeasurementDto
							{
                                Code = o.Code,
                                Name = o.Name,
                                Id = o.Id
							}
						 });


            var unitOfMeasurementListDtos = await query.ToListAsync();

            return _unitOfMeasurementsExcelExporter.ExportToFile(unitOfMeasurementListDtos);
         }


    }
}