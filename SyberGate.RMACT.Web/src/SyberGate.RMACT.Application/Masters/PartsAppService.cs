using SyberGate.RMACT.Masters;
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
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SyberGate.RMACT.Install;
using SyberGate.RMACT.Configuration;
using System.IO;
using SyberGate.RMACT.Authorization.Users;
using Abp.UI;
using SyberGate.RMACT.Storage;
using Abp.BackgroundJobs;
using SyberGate.RMACT.Masters.Importing;

namespace SyberGate.RMACT.Masters
{
	[AbpAuthorize(AppPermissions.Pages_Administration_Parts)]
    public class PartsAppService : RMACTAppServiceBase, IPartsAppService
    {
        protected readonly IBackgroundJobManager BackgroundJobManager;
        private readonly IConfigurationRoot _appImportConfiguration;
        private readonly UserManager _userManager;
        private readonly IRepository<SubPart> _subPartRepository;
        private readonly IRepository<Part> _partRepository;
		 private readonly IPartsExcelExporter _partsExcelExporter;
		 private readonly IRepository<Supplier,int> _lookup_supplierRepository;
		 private readonly IRepository<Buyer,int> _lookup_buyerRepository;
		 private readonly IRepository<RawMaterialGrade, int> _lookup_rmGroupRepository;
        private readonly IRepository<RawMaterialGrade, int> _lookup_rmGradeRepository;
        private readonly IPartsTemplateExcelExporter _IPartsTemplateExcelExporter;
        private readonly IRepository<RawMaterialGrade> _rawMaterialGradeRepository;
        private readonly InstallAppService _appConfiguration;

        public PartsAppService(IRepository<Part> partRepository, IPartsExcelExporter partsExcelExporter , IRepository<Supplier, int> lookup_supplierRepository, IRepository<Buyer, int> lookup_buyerRepository, IRepository<RawMaterialGrade, int> lookup_rmGroupRepository, IRepository<RawMaterialGrade> rawMaterialGradeRepository, IPartsTemplateExcelExporter IPartsTemplateExcelExporter, InstallAppService appConfiguration, IRepository<RawMaterialGrade, int> lookup_rmGradeRepository, IRepository<SubPart> subPartRepository, IAppConfigurationAccessor ConfigurationAccessor, IBackgroundJobManager backgroundJobManager) 
		  {
			_partRepository = partRepository;
			_partsExcelExporter = partsExcelExporter;
			_lookup_supplierRepository = lookup_supplierRepository;
		_lookup_buyerRepository = lookup_buyerRepository;
		_lookup_rmGroupRepository = lookup_rmGroupRepository;
            _rawMaterialGradeRepository = rawMaterialGradeRepository;
            _IPartsTemplateExcelExporter = IPartsTemplateExcelExporter;
            _appConfiguration = appConfiguration;
            _lookup_rmGradeRepository = lookup_rmGradeRepository;
            _subPartRepository = subPartRepository;
            _appImportConfiguration = ConfigurationAccessor.Configuration;
            _userManager = UserManager;
            BackgroundJobManager = backgroundJobManager;
          }

		 public async Task<PagedResultDto<GetPartForViewDto>> GetAll(GetAllPartsInput input)
         {
			
			var filteredParts = _partRepository.GetAll()
						.Include( e => e.SupplierFk)
						.Include( e => e.BuyerFk)
						.Include( e => e.RMGroupFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PartNo.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PartNoFilter),  e => e.PartNo == input.PartNoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SupplierNameFilter), e => e.SupplierFk != null && e.SupplierFk.Name == input.SupplierNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BuyerNameFilter), e => e.BuyerFk != null && e.BuyerFk.Name == input.BuyerNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.RMGroupNameFilter), e => e.RMGroupFk != null && e.RMGroupFk.Name == input.RMGroupNameFilter);

			var pagedAndFilteredParts = filteredParts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var parts = from o in pagedAndFilteredParts
                         join o1 in _lookup_supplierRepository.GetAll() on o.SupplierId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_buyerRepository.GetAll() on o.BuyerId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_rmGroupRepository.GetAll() on o.RMGroupId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         select new GetPartForViewDto() {
							Part = new PartDto
							{
                                GrossInputWeight = o.GrossInputWeight,
                                CastingForgingWeight = o.CastingForgingWeight,
                                FinishedWeight = o.FinishedWeight,
                                ScrapRecoveryPercent = o.ScrapRecoveryPercent,
                                PartNo = o.PartNo,
                                Description = o.Description,
                                Id = o.Id
							},
                         	SupplierName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                         	BuyerName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                         	RMGroupName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
						};

            var totalCount = await filteredParts.CountAsync();

            return new PagedResultDto<GetPartForViewDto>(
                totalCount,
                await parts.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_Parts_Edit)]
		 public async Task<GetPartForEditOutput> GetPartForEdit(EntityDto input)
         {
            var part = await _partRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPartForEditOutput {Part = ObjectMapper.Map<CreateOrEditPartDto>(part)};

		    if (output.Part.SupplierId != null)
            {
                var _lookupSupplier = await _lookup_supplierRepository.FirstOrDefaultAsync((int)output.Part.SupplierId);
                output.SupplierName = _lookupSupplier?.Name?.ToString();
            }

		    if (output.Part.BuyerId != null)
            {
                var _lookupBuyer = await _lookup_buyerRepository.FirstOrDefaultAsync((int)output.Part.BuyerId);
                output.BuyerName = _lookupBuyer?.Name?.ToString();
            }

		    if (output.Part.RMGroupId != null)
            {
                var _lookupRMGroup = await _lookup_rmGroupRepository.FirstOrDefaultAsync((int)output.Part.RMGroupId);
                output.RMGroupName = _lookupRMGroup?.Name?.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPartDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Parts_Create)]
		 protected virtual async Task Create(CreateOrEditPartDto input)
         {
            var part = ObjectMapper.Map<Part>(input);

			

            await _partRepository.InsertAsync(part);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Parts_Create)]
		 protected virtual async Task Update(CreateOrEditPartDto input)
         {
            var part = await _partRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, part);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_Parts_Delete)]
         public async Task Delete(EntityDto input)
         {
            var subparts = _subPartRepository.GetAll().Where(w => w.ParentPartId == input.Id);
            foreach(var subpart in subparts)
            {
                await _subPartRepository.DeleteAsync(subpart.Id);
            }
            await _partRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPartsToExcel(GetAllPartsForExcelInput input)
         {
			
			var filteredParts = _partRepository.GetAll()
						.Include( e => e.SupplierFk)
						.Include( e => e.BuyerFk)
						.Include( e => e.RMGroupFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PartNo.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PartNoFilter),  e => e.PartNo == input.PartNoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SupplierNameFilter), e => e.SupplierFk != null && e.SupplierFk.Name == input.SupplierNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.BuyerNameFilter), e => e.BuyerFk != null && e.BuyerFk.Name == input.BuyerNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.RMGroupNameFilter), e => e.RMGroupFk != null && e.RMGroupFk.Name == input.RMGroupNameFilter);

			var query = (from o in filteredParts
                         
                         select new GetPartForViewDto() { 
							Part = new PartDto
							{
                                GrossInputWeight = o.GrossInputWeight,
                                CastingForgingWeight = o.CastingForgingWeight,
                                FinishedWeight = o.FinishedWeight,
                                ScrapRecoveryPercent = o.ScrapRecoveryPercent,
                                PartNo = o.PartNo,
                                Description = o.Description,
                                Id = o.Id
							},
                         	SupplierName = o.SupplierFk == null || o.SupplierFk.Name == null ? "" : o.SupplierFk.Name.ToString()+"-"+ o.SupplierFk.Code.ToString(),
                         	BuyerName = o.BuyerFk == null || o.BuyerFk.Name == null ? "" : o.BuyerFk.Name.ToString(),
                         	RMGroupName = o.BuyerFk == null || o.BuyerFk.Name == null ? "" : o.BuyerFk.Name.ToString()
						 });


            var partListDtos = await query.ToListAsync();

            return _partsExcelExporter.ExportToFile(partListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Administration_Parts)]
         public async Task<PagedResultDto<PartSupplierLookupTableDto>> GetAllSupplierForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_supplierRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name != null && e.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var supplierList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PartSupplierLookupTableDto>();
			foreach(var supplier in supplierList){
				lookupTableDtoList.Add(new PartSupplierLookupTableDto
				{
					Id = supplier.Id,
					DisplayName = supplier.Name?.ToString()
				});
			}

            return new PagedResultDto<PartSupplierLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_Administration_Parts)]
         public async Task<PagedResultDto<PartBuyerLookupTableDto>> GetAllBuyerForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_buyerRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name != null && e.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var buyerList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PartBuyerLookupTableDto>();
			foreach(var buyer in buyerList){
				lookupTableDtoList.Add(new PartBuyerLookupTableDto
				{
					Id = buyer.Id,
					DisplayName = buyer.Name?.ToString()
				});
			}

            return new PagedResultDto<PartBuyerLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_Administration_Parts)]
         public async Task<PagedResultDto<PartRMGroupLookupTableDto>> GetAllRMGroupForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_rmGroupRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name != null && e.Name.Contains(input.Filter) && e.IsGroup == true
                );

            var totalCount = await query.CountAsync();

            var rmGroupList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PartRMGroupLookupTableDto>();
			foreach(var rmGroup in rmGroupList){
				lookupTableDtoList.Add(new PartRMGroupLookupTableDto
				{
					Id = rmGroup.Id,
					DisplayName = rmGroup.Name?.ToString()
				});
			}

            return new PagedResultDto<PartRMGroupLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

        public async Task GetGlobusDataExcels(string bomPath)
        {
            var filelist = Directory.GetFiles(bomPath);
            var admin = _userManager.GetAdminAsync();

            foreach (var filepath in filelist)
            {
                if (!File.Exists(filepath))
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }



                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);

                var tenantId = AbpSession.TenantId;
                var fileObject = new BinaryObject(tenantId, fileBytes, $"{DateTime.UtcNow} import from excel file.");
                //BinaryObjectManager.Save(fileObject);



                var args = new ImportGlobusDatasFromExcelJobArgs
                {
                    TenantId = tenantId,
                    BinaryObjectId = fileObject.Id,
                    User = admin.Result.ToUserIdentifier(),
                    Bytes = fileBytes
                };

                await BackgroundJobManager.EnqueueAsync<ImportGlobusDatasToExcelJob, ImportGlobusDatasFromExcelJobArgs>(args);


                //_importBOMsToExcelJob.ExecuteSync(args);



            }

        }

            public async Task<FileDto> GetPartsTemplate(GetAllPartsForExcelInput input)
        {
            var path = _appConfiguration.GetTemplatePath();

            var filteredSubParts = _subPartRepository.GetAll()
                        .Include(e => e.SupplierFk)
                        .Include(e => e.BuyerFk)
                        .Include(e => e.RMGroupFk)
                        .Include(e => e.RMGradeFk)
                        .Include(e => e.PartFk)
                        .Where(w => w.BuyerId == input.BuyerId && w.SupplierId == input.SupplierId);

            var subPartQuery = (from o in filteredSubParts
                                select new GetSubPartForViewDto()
                                {
                                    Part = new SubPartDto
                                    {
                                        GrossInputWeight = o.GrossInputWeight,
                                        CastingForgingWeight = o.CastingForgingWeight,
                                        FinishedWeight = o.FinishedWeight,
                                        ScrapRecoveryPercent = o.ScrapRecoveryPercent,
                                        PartNo = o.PartNo,
                                        Description = o.Description,
                                        Id = o.Id,
                                        ParentPartNo = o.ParentPartNo,
                                        RMReferenceCost = o.RMReferenceCost,
                                        RMReference = o.RMReference
                                    },
                                    SupplierName = o.SupplierFk == null || o.SupplierFk.Name == null ? "" : o.SupplierFk.Name.ToString(),
                                    BuyerName = o.BuyerFk == null || o.BuyerFk.Name == null ? "" : o.BuyerFk.Name.ToString(),
                                    RMGroupName = o.RMGroupFk == null || o.RMGroupFk.Name == null ? "" : o.RMGroupFk.Name.ToString(),
                                    RMGradeName = o.RMGradeFk == null || o.RMGradeFk.Name == null ? "" : o.RMGradeFk.Name.ToString()
                                });
            var subPartListDtos = await subPartQuery.ToListAsync();

            var filteredParts = _partRepository.GetAll()
                        .Include(e => e.SupplierFk)
                        .Include(e => e.BuyerFk)
                        .Include(e => e.RMGroupFk)
                        .Include(e => e.RMGradeFk)
                        .Where(w => w.BuyerId == input.BuyerId && w.SupplierId == input.SupplierId);

            var query = (from o in filteredParts
                         
                         select new GetPartForViewDto()
                         {
                             Part = new PartDto
                             {
                                 GrossInputWeight = o.GrossInputWeight,
                                 CastingForgingWeight = o.CastingForgingWeight,
                                 FinishedWeight = o.FinishedWeight,
                                 ScrapRecoveryPercent = o.ScrapRecoveryPercent * 100,
                                 PartNo = o.PartNo,
                                 Description = o.Description,
                                 Id = o.Id,
                                 IsParent = o.IsParent,
                                 RMReferenceCost = o.RMReferenceCost,
                                 RMReference = o.RMReference
                             },
                             SupplierName = o.SupplierFk == null || o.SupplierFk.Name == null ? "" : o.SupplierFk.Name.ToString(),
                             BuyerName = o.BuyerFk == null || o.BuyerFk.Name == null ? "" : o.BuyerFk.Name.ToString(),
                             RMGroupName = o.RMGroupFk == null || o.RMGroupFk.Name == null ? "" : o.RMGroupFk.Name.ToString(),
                             RMGradeName = o.RMGradeFk == null || o.RMGradeFk.Name == null ? "" : o.RMGradeFk.Name.ToString()
                         });


            var partListDtos = await query.ToListAsync();

            var filteredRawMaterialGrades = _rawMaterialGradeRepository.GetAll().Where(w=> w.IsGroup == false);

            var rmquery = (from o in filteredRawMaterialGrades
                         select new GetRawMaterialGradeForViewDto()
                         {
                             RawMaterialGrade = new RawMaterialGradeDto
                             {
                                 Name = o.Name,
                                 Id = o.Id,
                                 RMGroupName =  ((o.RawMaterialGradeId ?? 0) == 0) ? "" : o.RawMaterialGradeFk.Name
                             }
                         });


            var rawMaterialGradeListDtos = await rmquery.ToListAsync();

            var filteredRMGroups = _lookup_rmGroupRepository.GetAll().Where(w=> w.IsGroup == true);
            var rmGroup = (from o in filteredRMGroups
                           select new GetRawMaterialGradeForViewDto()
                           {
                               RawMaterialGrade = new RawMaterialGradeDto
                               {
                                   Name = o.Name,
                                   Id = o.Id
                               }
                           });
            var rmGroupListDtos = await rmGroup.ToListAsync();

            var _lookupSupplier = await _lookup_supplierRepository.FirstOrDefaultAsync((int)input.SupplierId);
            input.SupplierNameFilter = _lookupSupplier?.Name?.ToString() + "-" + _lookupSupplier?.Code.ToString();

            var _lookupBuyer = await _lookup_buyerRepository.FirstOrDefaultAsync((int)input.BuyerId);
            input.BuyerNameFilter = _lookupBuyer?.Name?.ToString();

            List<GetPartForViewDto> partlist = new List<GetPartForViewDto>();

            foreach(var part in partListDtos)
            {
                partlist.Add(part);
                if (part.Part.IsParent ?? false)
                {
                    foreach(var subPart in subPartListDtos)
                    {
                        if (subPart.Part.ParentPartNo == part.Part.PartNo)
                        {
                            var _subpart  = ObjectMapper.Map<PartDto>(subPart.Part);
                            if (subPart.Part.PartNo == subPart.Part.ParentPartNo)
                            {
                                _subpart.PartNo = "";
                                _subpart.Description = "";
                                _subpart.SubPartDescription = "";
                            }
                            else
                            {
                                _subpart.SubPartNo = subPart.Part.PartNo;
                                _subpart.SubPartDescription = subPart.Part.Description;
                                _subpart.PartNo = "";
                                _subpart.Description = "";
                            }
                            partlist.Add(new GetPartForViewDto() { Part = _subpart, BuyerName = subPart.BuyerName, RMGradeName = subPart.RMGradeName, RMGroupName = subPart.RMGroupName, SupplierName = subPart.SupplierName });
                        }
                    }
                }
            }

            return _IPartsTemplateExcelExporter.ExportToFile(partlist, rawMaterialGradeListDtos, rmGroupListDtos, input.SupplierNameFilter, input.BuyerNameFilter, path + input.TemplatePath);
        }
       
    }
}