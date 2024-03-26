using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters;
using System.Collections.Generic;
using SyberGate.RMACT.Masters;
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
using Abp.Runtime.Session;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using SyberGate.RMACT.Authorization.Users;

namespace SyberGate.RMACT.Masters
{
	[AbpAuthorize(AppPermissions.Pages_Administration_BaseRMRates)]
	public class BaseRMRatesAppService : RMACTAppServiceBase, IBaseRMRatesAppService
	{
		private readonly UserManager _userManager;
		private readonly IRepository<BaseRMRate> _baseRMRateRepository;
		private readonly IBaseRMRatesExcelExporter _baseRMRatesExcelExporter;
		private readonly IRepository<RawMaterialGrade, int> _lookup_rmGroupRepository;
		private readonly IRepository<UnitOfMeasurement, int> _lookup_unitOfMeasurementRepository;
		private readonly IRepository<Year, int> _lookup_yearRepository;
		private readonly IRepository<Buyer, int> _lookup_buyerRepository;
		private readonly IRepository<Supplier, int> _lookup_supplierRepository;


		public BaseRMRatesAppService(IRepository<BaseRMRate> baseRMRateRepository, IBaseRMRatesExcelExporter baseRMRatesExcelExporter, IRepository<RawMaterialGrade, int> lookup_rmGroupRepository, IRepository<UnitOfMeasurement, int> lookup_unitOfMeasurementRepository, IRepository<Year, int> lookup_yearRepository, IRepository<Buyer, int> lookup_buyerRepository, IRepository<Supplier, int> lookup_supplierRepository, UserManager userManager)
		{
			_baseRMRateRepository = baseRMRateRepository;
			_baseRMRatesExcelExporter = baseRMRatesExcelExporter;
			_lookup_rmGroupRepository = lookup_rmGroupRepository;
			_lookup_unitOfMeasurementRepository = lookup_unitOfMeasurementRepository;
			_lookup_yearRepository = lookup_yearRepository;
			_lookup_buyerRepository = lookup_buyerRepository;
			_lookup_supplierRepository = lookup_supplierRepository;
			_userManager = userManager;

		}

		public async Task<PagedResultDto<GetBaseRMRateForViewDto>> GetAll(GetAllBaseRMRatesInput input)
		{

			var filteredBaseRMRates = _baseRMRateRepository.GetAll()
						.Include(e => e.RMGroupFk)
						.Include(e => e.UnitOfMeasurementFk)
						.Include(e => e.YearFk)
						.Include(e => e.BuyerFk)
						.Include(e => e.SupplierFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.RMGroupFk.Name.Contains(input.Filter) || e.YearFk.Name.Contains(input.Filter) || e.BuyerFk.Name.Contains(input.Filter) || e.SupplierFk.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RMGroupNameFilter), e => e.RMGroupFk != null && e.RMGroupFk.Name == input.RMGroupNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UnitOfMeasurementCodeFilter), e => e.UnitOfMeasurementFk != null && e.UnitOfMeasurementFk.Code == input.UnitOfMeasurementCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.YearNameFilter), e => e.YearFk != null && e.YearFk.Name == input.YearNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BuyerNameFilter), e => e.BuyerFk != null && e.BuyerFk.Name == input.BuyerNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SupplierNameFilter), e => e.SupplierFk != null && e.SupplierFk.Name == input.SupplierNameFilter);

			var pagedAndFilteredBaseRMRates = filteredBaseRMRates
				.OrderBy(input.Sorting ?? "id asc")
				.PageBy(input);

			var baseRMRates = from o in pagedAndFilteredBaseRMRates
							  join o1 in _lookup_rmGroupRepository.GetAll() on o.RMGroupId equals o1.Id into j1
							  from s1 in j1.DefaultIfEmpty()

							  join o2 in _lookup_unitOfMeasurementRepository.GetAll() on o.UnitOfMeasurementId equals o2.Id into j2
							  from s2 in j2.DefaultIfEmpty()

							  join o3 in _lookup_yearRepository.GetAll() on o.YearId equals o3.Id into j3
							  from s3 in j3.DefaultIfEmpty()

							  join o4 in _lookup_buyerRepository.GetAll() on o.BuyerId equals o4.Id into j4
							  from s4 in j4.DefaultIfEmpty()

							  join o5 in _lookup_supplierRepository.GetAll() on o.SupplierId equals o5.Id into j5
							  from s5 in j5.DefaultIfEmpty()

							  select new GetBaseRMRateForViewDto()
							  {
								  BaseRMRate = new BaseRMRateDto
								  {
									  UnitRate = o.UnitRate,
									  ScrapPercent = o.ScrapPercent,
									  ScrapAmount = o.ScrapAmount,
									  Month = o.Month,
									  Id = o.Id,
									  IsApproved = o.IsApproved == null ? false : true,
									  IndexName = o.IndexName,
									  FromPeriod = o.FromPeriod,
									  ToPeriod = o.ToPeriod,
									  IndexValue = o.IndexValue,
									  WeightRatio = o.WeightRatio,
									  LossRatio = o.LossRatio
								  },
								  RMGroupName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
								  UnitOfMeasurementCode = s2 == null || s2.Code == null ? "" : s2.Code.ToString(),
								  YearName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
								  BuyerName = s4 == null || s4.Name == null ? "" : s4.Name.ToString(),
								  SupplierName = s5 == null || s5.Name == null ? "" : s5.Name.ToString()
							  };

			var totalCount = await filteredBaseRMRates.CountAsync();

			return new PagedResultDto<GetBaseRMRateForViewDto>(
				totalCount,
				await baseRMRates.ToListAsync()
			);
		}

		[AbpAuthorize(AppPermissions.Pages_Administration_BaseRMRates_Edit)]
		public async Task<GetBaseRMRateForEditOutput> GetBaseRMRateForEdit(EntityDto input)
		{
			var baseRMRate = await _baseRMRateRepository.FirstOrDefaultAsync(input.Id);

			var output = new GetBaseRMRateForEditOutput { BaseRMRate = ObjectMapper.Map<CreateOrEditBaseRMRateDto>(baseRMRate) };

			if (output.BaseRMRate.RMGroupId != null)
			{
				var _lookupRMGroup = await _lookup_rmGroupRepository.FirstOrDefaultAsync((int)output.BaseRMRate.RMGroupId);
				output.RMGroupName = _lookupRMGroup?.Name?.ToString();
			}

			if (output.BaseRMRate.UnitOfMeasurementId != null)
			{
				var _lookupUnitOfMeasurement = await _lookup_unitOfMeasurementRepository.FirstOrDefaultAsync((int)output.BaseRMRate.UnitOfMeasurementId);
				output.UnitOfMeasurementCode = _lookupUnitOfMeasurement?.Code?.ToString();
			}

			if (output.BaseRMRate.YearId != null)
			{
				var _lookupYear = await _lookup_yearRepository.FirstOrDefaultAsync((int)output.BaseRMRate.YearId);
				output.YearName = _lookupYear?.Name?.ToString();
			}

			if (output.BaseRMRate.BuyerId != null)
			{
				var _lookupBuyer = await _lookup_buyerRepository.FirstOrDefaultAsync((int)output.BaseRMRate.BuyerId);
				output.BuyerName = _lookupBuyer?.Name?.ToString();
			}

			if (output.BaseRMRate.SupplierId != null)
			{
				var _lookupSupplier = await _lookup_supplierRepository.FirstOrDefaultAsync((int)output.BaseRMRate.SupplierId);
				output.SupplierName = _lookupSupplier?.Name?.ToString();
			}

			return output;
		}

		public async Task CreateOrEdit(CreateOrEditBaseRMRateDto input)
		{
			if (input.Id == null)
			{
				await Create(input);
			}
			else
			{
				await Update(input);
			}
		}

		[AbpAuthorize(AppPermissions.Pages_Administration_BaseRMRates_Create)]
		protected virtual async Task Create(CreateOrEditBaseRMRateDto input)
		{
			var baseRMRate = ObjectMapper.Map<BaseRMRate>(input);



			await _baseRMRateRepository.InsertAsync(baseRMRate);
		}

		[AbpAuthorize(AppPermissions.Pages_Administration_BaseRMRates_Edit)]
		protected virtual async Task Update(CreateOrEditBaseRMRateDto input)
		{
			var baseRMRate = await _baseRMRateRepository.FirstOrDefaultAsync((int)input.Id);
			ObjectMapper.Map(input, baseRMRate);
		}

		[AbpAuthorize(AppPermissions.Pages_Administration_BaseRMRates_Delete)]
		public async Task Delete(EntityDto input)
		{
			await _baseRMRateRepository.DeleteAsync(input.Id);
		}

		public async Task<FileDto> GetBaseRMRatesToExcel(GetAllBaseRMRatesForExcelInput input)
		{

			var filteredBaseRMRates = _baseRMRateRepository.GetAll()
						.Include(e => e.RMGroupFk)
						.Include(e => e.UnitOfMeasurementFk)
						.Include(e => e.YearFk)
						.Include(e => e.BuyerFk)
						.Include(e => e.SupplierFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
						.WhereIf(!string.IsNullOrWhiteSpace(input.RMGroupNameFilter), e => e.RMGroupFk != null && e.RMGroupFk.Name == input.RMGroupNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UnitOfMeasurementCodeFilter), e => e.UnitOfMeasurementFk != null && e.UnitOfMeasurementFk.Code == input.UnitOfMeasurementCodeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.YearNameFilter), e => e.YearFk != null && e.YearFk.Name == input.YearNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BuyerNameFilter), e => e.BuyerFk != null && e.BuyerFk.Name == input.BuyerNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SupplierNameFilter), e => e.SupplierFk != null && e.SupplierFk.Name == input.SupplierNameFilter);

			var query = (from o in filteredBaseRMRates
						 join o1 in _lookup_rmGroupRepository.GetAll() on o.RMGroupId equals o1.Id into j1
						 from s1 in j1.DefaultIfEmpty()

						 join o2 in _lookup_unitOfMeasurementRepository.GetAll() on o.UnitOfMeasurementId equals o2.Id into j2
						 from s2 in j2.DefaultIfEmpty()

						 join o3 in _lookup_yearRepository.GetAll() on o.YearId equals o3.Id into j3
						 from s3 in j3.DefaultIfEmpty()

						 join o4 in _lookup_buyerRepository.GetAll() on o.BuyerId equals o4.Id into j4
						 from s4 in j4.DefaultIfEmpty()

						 join o5 in _lookup_supplierRepository.GetAll() on o.SupplierId equals o5.Id into j5
						 from s5 in j5.DefaultIfEmpty()

						 select new GetBaseRMRateForViewDto()
						 {
							 BaseRMRate = new BaseRMRateDto
							 {
								 UnitRate = o.UnitRate,
								 ScrapPercent = o.ScrapPercent,
								 ScrapAmount = o.ScrapAmount,
								 Month = o.Month,
								 Id = o.Id,
								 IsApproved = o.IsApproved == null ? false : true,
								 IndexName = o.IndexName,
								 FromPeriod = o.FromPeriod,
								 ToPeriod = o.ToPeriod,
								 IndexValue = o.IndexValue,
								 WeightRatio = o.WeightRatio,
								 LossRatio = o.LossRatio
							 },
							 RMGroupName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
							 UnitOfMeasurementCode = s2 == null || s2.Code == null ? "" : s2.Code.ToString(),
							 YearName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
							 BuyerName = s4 == null || s4.Name == null ? "" : s4.Name.ToString(),
							 SupplierName = s5 == null || s5.Name == null ? "" : s5.Name.ToString()
						 });


			var baseRMRateListDtos = await query.ToListAsync();

			return _baseRMRatesExcelExporter.ExportToFile(baseRMRateListDtos);
		}



		[AbpAuthorize(AppPermissions.Pages_Administration_BaseRMRates)]
		public async Task<PagedResultDto<BaseRMRateRMGroupLookupTableDto>> GetAllRMGroupForLookupTable(GetAllForLookupTableInput input)
		{
			var query = _lookup_rmGroupRepository.GetAll().WhereIf(
				   !string.IsNullOrWhiteSpace(input.Filter),
				  e => e.Name != null && e.Name.Contains(input.Filter) && e.IsGroup == true
			   );

			var totalCount = await query.CountAsync();

			var rmGroupList = await query
				.PageBy(input)
				.ToListAsync();

			var lookupTableDtoList = new List<BaseRMRateRMGroupLookupTableDto>();
			foreach (var rmGroup in rmGroupList)
			{
				lookupTableDtoList.Add(new BaseRMRateRMGroupLookupTableDto
				{
					Id = rmGroup.Id,
					DisplayName = rmGroup.Name?.ToString()
				});
			}

			return new PagedResultDto<BaseRMRateRMGroupLookupTableDto>(
				totalCount,
				lookupTableDtoList
			);
		}
		[AbpAuthorize(AppPermissions.Pages_Administration_BaseRMRates)]
		public async Task<List<BaseRMRateUnitOfMeasurementLookupTableDto>> GetAllUnitOfMeasurementForTableDropdown()
		{
			return await _lookup_unitOfMeasurementRepository.GetAll()
				.Select(unitOfMeasurement => new BaseRMRateUnitOfMeasurementLookupTableDto
				{
					Id = unitOfMeasurement.Id,
					DisplayName = unitOfMeasurement == null || unitOfMeasurement.Code == null ? "" : unitOfMeasurement.Code.ToString()
				}).ToListAsync();
		}

		[AbpAuthorize(AppPermissions.Pages_Administration_BaseRMRates)]
		public async Task<List<BaseRMRateYearLookupTableDto>> GetAllYearForTableDropdown()
		{
			return await _lookup_yearRepository.GetAll()
				.Select(year => new BaseRMRateYearLookupTableDto
				{
					Id = year.Id,
					DisplayName = year == null || year.Name == null ? "" : year.Name.ToString()
				}).ToListAsync();
		}


		[AbpAuthorize(AppPermissions.Pages_Administration_BaseRMRates)]
		public async Task<PagedResultDto<BaseRMRateBuyerLookupTableDto>> GetAllBuyerForLookupTable(GetAllForLookupTableInput input)
		{
			var query = _lookup_buyerRepository.GetAll().WhereIf(
				   !string.IsNullOrWhiteSpace(input.Filter),
				  e => e.Name != null && e.Name.Contains(input.Filter)
			   );

			var totalCount = await query.CountAsync();

			var buyerList = await query
				.PageBy(input)
				.ToListAsync();

			var lookupTableDtoList = new List<BaseRMRateBuyerLookupTableDto>();
			foreach (var buyer in buyerList)
			{
				lookupTableDtoList.Add(new BaseRMRateBuyerLookupTableDto
				{
					Id = buyer.Id,
					DisplayName = buyer.Name?.ToString()
				});
			}

			return new PagedResultDto<BaseRMRateBuyerLookupTableDto>(
				totalCount,
				lookupTableDtoList
			);
		}

		[AbpAuthorize(AppPermissions.Pages_Administration_BaseRMRates)]
		public async Task<PagedResultDto<BaseRMRateSupplierLookupTableDto>> GetAllSupplierForLookupTable(GetAllForLookupTableInput input)
		{
			User user = _userManager.GetUser(AbpSession.ToUserIdentifier());
			var logedinUser = AbpSession.GetUserId();

			var query = _lookup_supplierRepository.GetAll().WhereIf(
					!string.IsNullOrWhiteSpace(input.Filter),
				   e => e.Name != null && e.Name.Contains(input.Filter)
				);

			var totalCount = await query.CountAsync();

			var supplierList = await query
				.PageBy(input)
				.ToListAsync();

			var lookupTableDtoList = new List<BaseRMRateSupplierLookupTableDto>();
			foreach (var supplier in supplierList)
			{
				lookupTableDtoList.Add(new BaseRMRateSupplierLookupTableDto
				{
					Id = supplier.Id,
					DisplayName = supplier.Name?.ToString()
				});
			}

			return new PagedResultDto<BaseRMRateSupplierLookupTableDto>(
				totalCount,
				lookupTableDtoList
			);
		}
	}
}