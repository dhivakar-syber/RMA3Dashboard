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
using Microsoft.EntityFrameworkCore.Internal;

namespace SyberGate.RMACT.Masters.Importing
{

    public class ImportPartModelMatrixToExcelJob : BackgroundJob<ImportPartModelMatrixFromJobArgs>, ITransientDependency
    {
        private readonly IRepository<PartModelMatrix> _partModelMatrixRepository;
        private readonly IRepository<LeadModel> _leadModelsRepository;
        private readonly IPartModelMatrixExcelDataReader _PartModelMatricExcelDataReader;
        private readonly IInvalidPartModelMatrixExporter _invalidPartModelMatrixExporter;
        private readonly IAppNotifier _appNotifier;
        private readonly IUserPolicy _userPolicy;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ImportPartModelMatrixToExcelJob(
            IPartModelMatrixExcelDataReader PartModelMatrixExcelDataReader,
            IInvalidPartModelMatrixExporter InvalidPartModelMatrixExporter,
            IAppNotifier appNotifier,
            IUserPolicy userPolicy,
            IBinaryObjectManager binaryObjectManager,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<PartModelMatrix> PartModelMatrixRepository,
            IRepository<LeadModel> LeadModelRepository)
        {
            _PartModelMatricExcelDataReader = PartModelMatrixExcelDataReader;
            _invalidPartModelMatrixExporter = InvalidPartModelMatrixExporter;
            _appNotifier = appNotifier;
            _userPolicy = userPolicy;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _partModelMatrixRepository = PartModelMatrixRepository;
            _leadModelsRepository = LeadModelRepository;
        }

        public override void Execute(ImportPartModelMatrixFromJobArgs args)
        {
            var PartModelMatrixes = GetLeadModelFromExcelOrNull(args);
            if (PartModelMatrixes == null || !PartModelMatrixes.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            CreateLeadModels(args, PartModelMatrixes);
        }



      

        public List<ImportPartModelMatrixDto> GetLeadModelFromExcelOrNull(ImportPartModelMatrixFromJobArgs args)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    try
                    {
                        if (args.Bytes != null)
                        {
                            return _PartModelMatricExcelDataReader.GetPartModelMatrixFromExcel(args.Bytes);
                        }
                        else
                        {
                            var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                            return _PartModelMatricExcelDataReader.GetPartModelMatrixFromExcel(file.Bytes);
                        }
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    finally
                    {
                        uow.Complete();
                    }
                }
            }
        }

        public void CreateLeadModels(ImportPartModelMatrixFromJobArgs args, List<ImportPartModelMatrixDto> PartModelMatrixes)
        {
            var invalidPartModelMatrix = new List<ImportPartModelMatrixDto>();


            foreach (var partModel in PartModelMatrixes)
            {
                using (var uow = _unitOfWorkManager.Begin())
                {
                    using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                    {
                        if (partModel.CanBeImported())
                        {
                            try
                            {
                                AsyncHelper.RunSync(() => UpdateLeadModelAsync(partModel));
                            }
                            catch (UserFriendlyException exception)
                            {
                                partModel.Exception = exception.Message;
                                invalidPartModelMatrix.Add(partModel);
                            }
                            catch (Exception exception)
                            {
                                partModel.Exception = exception.ToString();
                                invalidPartModelMatrix.Add(partModel);
                            }
                        }
                        else
                        {
                            invalidPartModelMatrix.Add(partModel);
                        }
                    }

                    uow.Complete();
                }
            }

            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    AsyncHelper.RunSync(() => ProcessImportLeadModelsResultAsync(args, invalidPartModelMatrix));
                }

                uow.Complete();
            }
        }

        private async Task CreateLeadMOdelsAsync(ImportPartModelMatrixDto input)
        {
            var tenantId = CurrentUnitOfWork.GetTenantId();

            if (tenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(tenantId.Value);
            }
            //ImportPartModelMatrixDto2 v= new ImportPartModelMatrixDto2();

            //v.PartNumber= input.PartNumber;
            //v.Quantity= input.Quantity;
            //v.LeadModelId= input.LeadModelId;

            var PartModelMatrix = _objectMapper.Map<PartModelMatrix>(input); //Passwords is not mapped (see mapping configuration)


            await _partModelMatrixRepository.InsertAsync(PartModelMatrix);
        }

        private async Task UpdateLeadModelAsync(ImportPartModelMatrixDto input)
        {
            var tenantId = CurrentUnitOfWork.GetTenantId();

            if (tenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(tenantId.Value);
            }

            

            
            var LeadModelid = _leadModelsRepository.GetAll().Where(w => w.Name == input.Name);
            var v=LeadModelid.FirstOrDefault().Id;
            var partModelMatrixes = _partModelMatrixRepository.GetAll().Where(w => w.PartNumber == input.PartNumber&&w.LeadModelId==v);


            if (partModelMatrixes.Count() == 0)
            {
                input.LeadModelId= v;
                CreateLeadMOdelsAsync(input);
            }
            else
            {
               foreach (var partModelMatrix in partModelMatrixes)
                {
                    input.LeadModelId = v;
                    _objectMapper.Map(input, partModelMatrix);
                }
            }

        }

        private async Task ProcessImportLeadModelsResultAsync(ImportPartModelMatrixFromJobArgs args,
            List<ImportPartModelMatrixDto> invalidPartModelMatrix)
        {
            if (invalidPartModelMatrix.Any())
            {
                var file = _invalidPartModelMatrixExporter.ExportToFile(invalidPartModelMatrix);
                await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    new LocalizableString("AllPartModelMatrixSuccessfullyImportedFromExcel", RMACTConsts.LocalizationSourceName),
                    null,
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        public void SendInvalidExcelNotification(ImportPartModelMatrixFromJobArgs args)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                        args.User,
                        new LocalizableString("FileCantBeConvertedToPartModelMatrixList", RMACTConsts.LocalizationSourceName),
                        null,
                        Abp.Notifications.NotificationSeverity.Warn));
                }
                uow.Complete();
            }
        }


    }
}

