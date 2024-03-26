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
    public class ImportPartsToExcelJob : BackgroundJob<ImportPartsFromExcelJobArgs>, ITransientDependency 
    {
        private readonly IRepository<SubPart> _subpartMasterDataRepository;
        private readonly IRepository<Part> _partMasterDataRepository;
        private readonly IPartExcelDataReader _partExcelDataReader;
        private readonly IInvalidPartExporter _invalidPartExporter;
        private readonly IAppNotifier _appNotifier;
        private readonly IUserPolicy _userPolicy;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<RawMaterialGrade> _rawMaterialGrade;
        private readonly IRepository<RawMaterialGrade> _rmGroup;
        private readonly IRepository<Supplier> _supplier;
        private readonly IRepository<Buyer> _buyer;

        public ImportPartsToExcelJob(IPartExcelDataReader partExcelDataReader, IRepository<SubPart> subpartMasterDataRepository,
        IRepository<Part> partMasterDataRepository,
            IInvalidPartExporter invalidPartExporter,
            IAppNotifier appNotifier,
            IUserPolicy userPolicy,
            IBinaryObjectManager binaryObjectManager,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<RawMaterialGrade> rawMaterialGrade,
            IRepository<RawMaterialGrade> rmGroup,
            IRepository<Supplier> supplier,
            IRepository<Buyer> buyer
             )
        {
            _partExcelDataReader = partExcelDataReader;
            _subpartMasterDataRepository = subpartMasterDataRepository;
            _partMasterDataRepository = partMasterDataRepository;
            _invalidPartExporter = invalidPartExporter;
            _appNotifier = appNotifier;
            _userPolicy = userPolicy;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _rawMaterialGrade = rawMaterialGrade;
            _rmGroup = rmGroup;
            _supplier = supplier;
            _buyer = buyer;
        }

        public override void Execute(ImportPartsFromExcelJobArgs args)
        {
            var parts = GetPartListFromExcelOrNull(args);
            if (parts == null || !parts.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            CreateParts(args, parts);
        }

        private List<ImportPartDto> GetPartListFromExcelOrNull(ImportPartsFromExcelJobArgs args)
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
                                return _partExcelDataReader.GetPartsFromExcel(args.Bytes);
                            }
                            else
                            {
                                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                                return _partExcelDataReader.GetPartsFromExcel(file.Bytes);
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

        private void CreateParts(ImportPartsFromExcelJobArgs args, List<ImportPartDto> parts)
        {
            var invalidParts = new List<ImportPartDto>();

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
                                AsyncHelper.RunSync(() => CreatePartAsync(part));
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
                    AsyncHelper.RunSync(() => ProcessImportPartsResultAsync(args, invalidParts));
                }

                uow.Complete();
            }
        }

        private async Task CreatePartAsync(ImportPartDto input)
        {
            var tenantId = CurrentUnitOfWork.GetTenantId();

            if (tenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(tenantId.Value);
            }

            var part = _objectMapper.Map<Part>(input); //Passwords is not mapped (see mapping configuration)

            var partsuppliercode = "";

            char[] separator = { '-' };
            string[] partresult = input.Supplier.Split(separator);

            partsuppliercode = partresult[partresult.Length - 1];


            //var partsFromDB = _partMasterDataRepository.GetAll().Where(e => e.PartNo == input.PartNo && e.SupplierFk.Name == input.Supplier && e.BuyerFk.Name == input.Buyer);
            var partsFromDB = _partMasterDataRepository.GetAll().Where(e => e.PartNo == input.PartNo && e.SupplierFk.Code == partsuppliercode && e.BuyerFk.Name == input.Buyer);
            var count = partsFromDB.Count();

            if (string.IsNullOrWhiteSpace(input.SubPartNo))
            {
                var isNewGroup = true;
                if (!string.IsNullOrWhiteSpace(input.RMGroup))
                {
                    var rGroup = _rmGroup.GetAll().Where(e => e.Name == input.RMGroup);
                    var rCount = rGroup.Count();
                    if (rCount == 0)
                    {
                        part.RMGroupFk = await _rmGroup.InsertAsync(new RawMaterialGrade { Name = input.RMGroup, IsGroup = true });
                        isNewGroup = true;
                    }
                    else
                    {
                        part.RMGroupId = rGroup.FirstOrDefault().Id;
                        isNewGroup = false;
                    }
                }

                if (!string.IsNullOrWhiteSpace(input.RMGrade))
                {
                    var rmGrade = _rawMaterialGrade.GetAll().Include(e => e.RawMaterialGradeFk).Where(e => e.Name == input.RMGrade && e.RawMaterialGradeFk.Name == input.RMGroup);
                    var gCount = rmGrade.Count();
                    if (gCount == 0)
                    {
                        if (isNewGroup)
                            part.RMGradeFk = await _rawMaterialGrade.InsertAsync(new RawMaterialGrade { Name = input.RMGrade, RawMaterialGradeFk = part.RMGroupFk });
                        else
                            part.RMGradeFk = await _rawMaterialGrade.InsertAsync(new RawMaterialGrade { Name = input.RMGrade, RawMaterialGradeId = part.RMGroupId });
                    }
                    else
                    {
                        part.RMGradeId = rmGrade.FirstOrDefault().Id;
                    }
                }


                //Grade = _rawMaterialGrade.GetAll().Where(e => e.Name == input.RMGrade).FirstOrDefault();

                //part.RawMaterialGradeId = Grade.Id;
               
                if (input.Supplier != "")
                {
                    

                    var supplier = _supplier.GetAll().Where(w => w.Code == partsuppliercode.ToString()) ;
                    

                    if (supplier.Count() == 0)
                    {

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

                    }
                    else
                    {
                        part.BuyerId = buyer.FirstOrDefault().Id;
                    }

                }

                if (count == 0)
                {
                    await _partMasterDataRepository.InsertAsync(part);
                }
                else
                {
                    var id = partsFromDB.FirstOrDefault().Id;
                    var updatepart = _partMasterDataRepository.GetAll().Where(w => w.Id == id).FirstOrDefault();
                    updatepart.RMGradeId = part.RMGradeId;
                    updatepart.RMGroupId = part.RMGroupId;
                    updatepart.GrossInputWeight = part.GrossInputWeight;
                    updatepart.CastingForgingWeight = part.CastingForgingWeight;
                    updatepart.FinishedWeight = part.FinishedWeight;
                    updatepart.ScrapRecoveryPercent = part.ScrapRecoveryPercent;
                    updatepart.RMReferenceCost = part.RMReferenceCost;
                    //updatepart.LogisticsCost = part.LogisticsCost;
                    updatepart.RMReference = part.RMReference;

                    await _partMasterDataRepository.UpdateAsync(updatepart);
                }
            }

            if (!string.IsNullOrWhiteSpace(input.SubPartNo))
            {
                var subpart = _objectMapper.Map<SubPart>(input);

                subpart.PartNo = input.SubPartNo;
                subpart.ParentPartNo = input.PartNo;

                if (count > 0)
                    subpart.ParentPartId = partsFromDB.FirstOrDefault().Id;

                var isNewGroup = true;
                if (!string.IsNullOrWhiteSpace(input.RMGroup))
                {
                    var rGroup = _rawMaterialGrade.GetAll().Where(e => e.Name == input.RMGroup);
                    var rCount = rGroup.Count();
                    if (rCount == 0)
                    {
                        subpart.RMGroupFk = await _rawMaterialGrade.InsertAsync(new RawMaterialGrade { Name = input.RMGroup, IsGroup = true });
                        isNewGroup = true;
                    }
                    else
                    {
                        subpart.RMGroupId = rGroup.FirstOrDefault().Id;
                        isNewGroup = false;
                    }
                }

                if (!string.IsNullOrWhiteSpace(input.RMGrade))
                {
                    var rmGrade = _rawMaterialGrade.GetAll().Where(e => e.Name == input.RMGrade && e.RawMaterialGradeFk.Name == input.RMGroup);
                    var gCount = rmGrade.Count();
                    if (gCount == 0)
                    {
                        if (isNewGroup)
                            subpart.RMGradeFk = await _rawMaterialGrade.InsertAsync(new RawMaterialGrade { Name = input.RMGrade, RawMaterialGradeFk = subpart.RMGroupFk});
                        else
                            subpart.RMGradeFk = await _rawMaterialGrade.InsertAsync(new RawMaterialGrade { Name = input.RMGrade, RawMaterialGradeId = (subpart.RMGroupId ?? 0) });
                    }
                    else
                    {
                        subpart.RMGradeId = rmGrade.FirstOrDefault().Id;
                    }
                }

                
                //if (input.Supplier != "")
                //{
                //    var supplier = _supplier.GetAll().Where(w => w.Name == input.Supplier);
                //    if (supplier.Count() == 0)
                //    {

                //    }
                //    else
                //    {
                //        subpart.SupplierId = supplier.FirstOrDefault().Id;
                //    }

                //}


                
                if (input.Supplier != "")
                {
                    

                    var supplier = _supplier.GetAll().Where(w => w.Code == partsuppliercode.ToString());


                    if (supplier.Count() == 0)
                    {

                    }
                    else
                    {
                        subpart.SupplierId = supplier.FirstOrDefault().Id;
                    }

                }



                if (input.Buyer != "")
                {
                    var buyer = _buyer.GetAll().Where(w => w.Name == input.Buyer);
                    if (buyer.Count() == 0)
                    {

                    }
                    else
                    {
                        subpart.BuyerId = buyer.FirstOrDefault().Id;
                    }

                }


                var subpartsFromDB = _subpartMasterDataRepository.GetAll().Where(e => e.PartNo == input.SubPartNo && e.SupplierFk.Code == partsuppliercode && e.BuyerFk.Name == input.Buyer && e.ParentPartNo == input.PartNo && e.RMGradeFk.Name == input.RMGrade);
                var subcount = subpartsFromDB.Count();
                if (subcount == 0)
                    await _subpartMasterDataRepository.InsertAsync(subpart);
                else
                {
                    var id = subpartsFromDB.FirstOrDefault().Id;
                    var updatepart = _subpartMasterDataRepository.GetAll().Where(w => w.Id == id).FirstOrDefault();
                    updatepart.RMGradeId = subpart.RMGradeId;
                    updatepart.RMGroupId = subpart.RMGroupId;
                    updatepart.GrossInputWeight = subpart.GrossInputWeight;
                    updatepart.CastingForgingWeight = subpart.CastingForgingWeight;
                    updatepart.FinishedWeight = subpart.FinishedWeight;
                    updatepart.ScrapRecoveryPercent = subpart.ScrapRecoveryPercent;
                    updatepart.RMReferenceCost = subpart.RMReferenceCost;
                    updatepart.RMReference = subpart.RMReference;

                    await _subpartMasterDataRepository.UpdateAsync(updatepart);
                }
            }

        }

        private async Task ProcessImportPartsResultAsync(ImportPartsFromExcelJobArgs args,
            List<ImportPartDto> invalidParts)
        {
            if (invalidParts.Any())
            {
                var file = _invalidPartExporter.ExportToFile(invalidParts);
                await _appNotifier.SomePartsCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
                Logger.Info("Part Data Import worker failed partially at " + Clock.Now);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    new LocalizableString("AllPartsSuccessfullyImportedFromExcel", RMACTConsts.LocalizationSourceName),
                    null,
                    Abp.Notifications.NotificationSeverity.Success);
                Logger.Info("Part Data Import worker completed at " + Clock.Now);
            }
        }


        private void SendInvalidExcelNotification(ImportPartsFromExcelJobArgs args)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                        args.User,
                        new LocalizableString("FileCantBeConvertedToPartList", RMACTConsts.LocalizationSourceName),
                        null,
                        Abp.Notifications.NotificationSeverity.Warn));
                }
                uow.Complete();
            }
        }

    }
}
