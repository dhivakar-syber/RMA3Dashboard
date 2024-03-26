using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.ObjectMapping;
using Abp.Threading;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Masters.Importing.Dto;
using SyberGate.RMACT.Authorization.Users;
using SyberGate.RMACT.Notifications;
using SyberGate.RMACT.Storage;
using Microsoft.EntityFrameworkCore;
using Abp.Timing;

namespace SyberGate.RMACT.Masters.Importing
{
    public class ImportGlobusDatasToExcelJob : BackgroundJob<ImportGlobusDatasFromExcelJobArgs>, ITransientDependency 
    {
        private readonly IRepository<GlobusData> _globusDataRepository;
        private readonly IGlobusDataExcelDataReader _globusDataExcelDataReader;
        private readonly IInvalidGlobusDataExporter _invalidGlobusDataExporter;
        private readonly IAppNotifier _appNotifier;
        private readonly IUserPolicy _userPolicy;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Supplier> _supplier;
        private readonly IRepository<Buyer> _buyer;
        private readonly IRepository<SupplierBuyerMap> _sbmap;
        private readonly IRepository<Plant> _plant;

        public ImportGlobusDatasToExcelJob(IGlobusDataExcelDataReader globusDataExcelDataReader, IRepository<GlobusData> globusDataRepository,
            IInvalidGlobusDataExporter invalidGlobusDataExporter,
            IAppNotifier appNotifier,
            IUserPolicy userPolicy,
            IBinaryObjectManager binaryObjectManager,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Supplier> supplier,
            IRepository<Buyer> buyer,
            IRepository<SupplierBuyerMap> sbmap,
            IRepository<Plant> plant
             )
        {
            _globusDataExcelDataReader = globusDataExcelDataReader;
            _globusDataRepository = globusDataRepository;
            _invalidGlobusDataExporter = invalidGlobusDataExporter;
            _appNotifier = appNotifier;
            _userPolicy = userPolicy;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _supplier = supplier;
            _buyer = buyer;
            _sbmap = sbmap;
            _plant = plant;
        }

        public override void Execute(ImportGlobusDatasFromExcelJobArgs args)
        {
            var globusDatas = GetGlobusDataListFromExcelOrNull(args);
            if (globusDatas == null || !globusDatas.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            CreateGlobusDatas(args, globusDatas);
        }

        private List<ImportGlobusDataDto> GetGlobusDataListFromExcelOrNull(ImportGlobusDatasFromExcelJobArgs args)
        {
            try
            {
                using (var uow = _unitOfWorkManager.Begin())
                {
                    using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                    {
                        try
                        {
                            if (args.Bytes != null)
                            {
                                return _globusDataExcelDataReader.GetGlobusDatasFromExcel(args.Bytes);
                            }
                            else
                            {
                                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                                return _globusDataExcelDataReader.GetGlobusDatasFromExcel(file.Bytes);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error while import", ex);
                            return null;
                        }
                        finally
                        {
                            uow.Complete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while import", ex);
            }
        }

        private void CreateGlobusDatas(ImportGlobusDatasFromExcelJobArgs args, List<ImportGlobusDataDto> parts)
        {
            var invalidParts = new List<ImportGlobusDataDto>();

            foreach (var part in parts)
            {

                using (var uow = _unitOfWorkManager.Begin())
                {
                    using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                    {
                        if (part.CanBeImported())
                        {
                            try
                            {
                                AsyncHelper.RunSync(() => CreateGlobusDataAsync(part));
                                AsyncHelper.RunSync(() => CreateSupplierBuyerMapAsync(part));
                                AsyncHelper.RunSync(() => CreatePlantAsync(part));
                            }
                            catch (UserFriendlyException exception)
                            {
                                part.Exception = exception.Message;
                                invalidParts.Add(part);
                            }
                            catch (Exception exception)
                            {
                                part.Exception = exception.ToString();
                                invalidParts.Add(part);
                            }
                        }
                        else
                        {
                            invalidParts.Add(part);
                        }
                    }

                    uow.Complete();
                }
            }

            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    AsyncHelper.RunSync(() => ProcessImportGlobusDatasResultAsync(args, invalidParts));
                }

                uow.Complete();
            }
        }

        private async Task CreateSupplierBuyerMapAsync(ImportGlobusDataDto input)
        {
            if (input.SupplierCode != "" && input.Buyer != "")
            {

                var part = _objectMapper.Map<GlobusData>(input); //Passwords is not mapped (see mapping configuration)

                var supbuymap = _sbmap.GetAll().Where(w => w.BuyerName == input.Buyer && w.SupplierName == input.SupplierName && w.SupplierCode==input.SupplierCode);
                var supplier = _supplier.GetAll().Where(w => w.Name == input.SupplierName && w.Code == input.SupplierCode);
                var buyer = _buyer.GetAll().Where(w => w.Name == input.Buyer);

                if (supbuymap.Count() == 0 && supplier.Count() > 0  && buyer.Count() > 0)
                {
                    var entsupbuy = new SupplierBuyerMap
                    {
                        BuyerCode = input.Buyer,
                        BuyerName = input.Buyer,
                        BuyerId = buyer.FirstOrDefault().Id,
                        SupplierCode = input.SupplierCode,
                        SupplierName = input.SupplierName,
                        SupplierId = supplier.FirstOrDefault().Id
                    };
                    await _sbmap.InsertAsync(entsupbuy);
                }
            }
        }

        private async Task CreatePlantAsync(ImportGlobusDataDto input)
        {
            if (input.PlantCode != "")
            {
                var plant = _plant.GetAll().Where(w => w.Code == input.PlantCode);
                if (plant.Count() == 0)
                {
                    await _plant.InsertAsync(new Plant { Code = input.PlantCode, Description = input.PlantDescription });
                }
            }
        }

        private async Task CreateGlobusDataAsync(ImportGlobusDataDto input)
        {
            var tenantId = CurrentUnitOfWork.GetTenantId();

            if (tenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(tenantId.Value);
            }

            var part = _objectMapper.Map<GlobusData>(input); //Passwords is not mapped (see mapping configuration)
            

            var partsFromDB = _globusDataRepository.GetAll().Where(
                e => e.PartNo == input.PartNo && 
                e.SupplierCode == input.SupplierCode && 
                e.Buyer == input.Buyer && 
                e.ContractNo == input.ContractNo &&
                e.FromDate == part.FromDate &&
                e.ToDate == part.ToDate &&
                e.PlantCode == part.PlantCode &&
                e.ES1 == part.ES1 && 
                e.ES2 == part.ES2);
            var count = partsFromDB.Count();

            if (count == 0)
            {

                if (input.SupplierCode != "")
                {
                   // var supplier = _supplier.GetAll().Where(w => w.Name == input.SupplierName && w.Code==input.SupplierCode) ;

                    var supplier = _supplier.GetAll().Where(w =>w.Code == input.SupplierCode);

                    if (supplier.Count() == 0)
                    {
                        //part.SupplierFk = new Supplier { Code = input.SupplierCode, Name = input.SupplierName };
                        part.SupplierFk = await _supplier.InsertAsync(new Supplier { Code = input.SupplierCode, Name = input.SupplierName });
                      //  part.SupplierId = part.SupplierFk.Id;

                    }
                    else
                    {
                        part.SupplierId = supplier.FirstOrDefault().Id;
                    }

                }
                if (input.Buyer != "")
                {
                    var buyer = _buyer.GetAll().Where(w => w.Name == input.Buyer);
                    if (buyer.Count() == 0)
                    {
                        part.BuyerFk = new Buyer { Code = input.Buyer, Name = input.Buyer };
                    }
                    else
                    {
                        part.BuyerId = buyer.FirstOrDefault().Id;
                    }

                }

                await _globusDataRepository.InsertAsync(part);
            }
            else
            {
                if (input.SupplierCode != "")
                {
                   // var supplier = _supplier.GetAll().Where(w => w.Name == input.SupplierName && w.Code==input.SupplierCode);

                    var supplier = _supplier.GetAll().Where(w =>w.Code == input.SupplierCode);

                    if (supplier.Count() == 0)
                    {
                        part.SupplierFk = await _supplier.InsertAsync(new Supplier { Code = input.SupplierCode, Name = input.SupplierName });

                        //part.SupplierId=part.SupplierFk.Id;
                       // part.SupplierId = await _supplier.InsertAsync(new Supplier { Code = input.SupplierCode, Name = input.SupplierName });
                    }
                    else
                    {
                        part.SupplierId = supplier.FirstOrDefault().Id;
                    }

                }
                if (input.Buyer != "")
                {
                    var buyer = _buyer.GetAll().Where(w => w.Name == input.Buyer);
                    if (buyer.Count() == 0)
                    {
                        part.BuyerFk = new Buyer { Code = input.Buyer, Name = input.Buyer };
                    }
                    else
                    {
                        part.BuyerId = buyer.FirstOrDefault().Id;
                    }

                }
                var upart = partsFromDB.FirstOrDefault();
                if (upart != null)
                {
                    
                    part.Id = upart.Id;

                    var procureData=await _globusDataRepository.FirstOrDefaultAsync((int)upart.Id);
                    
                    _objectMapper.Map(part, procureData);

                    procureData.CurrentExwPrice = part.CurrentExwPrice;
                    procureData.SupplierId= part.SupplierId;
                    procureData.SuppliserName= part.SuppliserName;
                    procureData.SOB = part.SOB;
                    procureData.PriceCurrency=part.PriceCurrency;
                    procureData.Uom=part.Uom;
                    procureData.PackagingCost=part.PackagingCost;
                    procureData.LogisticsCost=part.LogisticsCost;
                    procureData.EPU = part.EPU;
                    procureData.Description = part.Description;
                    



                    await _globusDataRepository.UpdateAsync(procureData);
                }
                
            }

        }

        private async Task ProcessImportGlobusDatasResultAsync(ImportGlobusDatasFromExcelJobArgs args,
            List<ImportGlobusDataDto> invalidGlobusDatas)
        {
            if (invalidGlobusDatas.Any())
            {
                var file = _invalidGlobusDataExporter.ExportToFile(invalidGlobusDatas);
                await _appNotifier.SomePartsCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
                Logger.Info("Procure Data Import worker failed partially at " + Clock.Now);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    new LocalizableString("AllGPartsSuccessfullyImportedFromExcel", RMACTConsts.LocalizationSourceName),
                    null,
                    Abp.Notifications.NotificationSeverity.Success);
                Logger.Info("Procure Data Import worker completed at " + Clock.Now);
            }
        }


        private void SendInvalidExcelNotification(ImportGlobusDatasFromExcelJobArgs args)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                        args.User,
                        new LocalizableString("FileCantBeConvertedToGlobusDataList", RMACTConsts.LocalizationSourceName),
                        null,
                        Abp.Notifications.NotificationSeverity.Warn));
                }
                uow.Complete();
            }
        }

    }
}
