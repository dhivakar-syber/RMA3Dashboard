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

    public class ImportLeadModelsToExcelJob : BackgroundJob<ImportLeadModelsFromExcelJobArgs>, ITransientDependency
    {
        private readonly IRepository<LeadModel> _leadModelsRepository;
        private readonly ILeadModelsExcelDataReader _LeadModelsExcelDataReader;
        private readonly IInvalidLeadModelsExporter _invalidLeadModelExporter;
        private readonly IAppNotifier _appNotifier;
        private readonly IUserPolicy _userPolicy;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ImportLeadModelsToExcelJob(
            ILeadModelsExcelDataReader LeadModelsExcelDataReader,
            IInvalidLeadModelsExporter InvalidLeadModelsExporter,
            IAppNotifier appNotifier,
            IUserPolicy userPolicy,
            IBinaryObjectManager binaryObjectManager,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<LeadModel> LeadModelsRepository)
        {
            _LeadModelsExcelDataReader = LeadModelsExcelDataReader;
            _invalidLeadModelExporter = InvalidLeadModelsExporter;
            _appNotifier = appNotifier;
            _userPolicy = userPolicy;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _leadModelsRepository = LeadModelsRepository;
        }

        public override void Execute(ImportLeadModelsFromExcelJobArgs args)
        {
            var LeadModels = GetLeadModelFromExcelOrNull(args);
            if (LeadModels == null || !LeadModels.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            CreateLeadModels(args, LeadModels);
        }

        

     
        public List<ImportLeadModelsDto> GetLeadModelFromExcelOrNull(ImportLeadModelsFromExcelJobArgs args)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    try
                    {
                        if (args.Bytes != null)
                        {
                            return _LeadModelsExcelDataReader.GetLeadModelsFromExcel(args.Bytes);
                        }
                        else
                        {
                            var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                            return _LeadModelsExcelDataReader.GetLeadModelsFromExcel(file.Bytes);
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

        public void CreateLeadModels(ImportLeadModelsFromExcelJobArgs args, List<ImportLeadModelsDto> leadModels)
        {
            var invalidleadModels = new List<ImportLeadModelsDto>();

            foreach (var leadmodel in leadModels)
            {
                using (var uow = _unitOfWorkManager.Begin())
                {
                    using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                    {
                        if (leadmodel.CanBeImported())
                        {
                            try
                            {
                                AsyncHelper.RunSync(() => UpdateLeadModelAsync(leadmodel));
                            }
                            catch (UserFriendlyException exception)
                            {
                                leadmodel.Exception = exception.Message;
                                invalidleadModels.Add(leadmodel);
                            }
                            catch (Exception exception)
                            {
                                leadmodel.Exception = exception.ToString();
                                invalidleadModels.Add(leadmodel);
                            }
                        }
                        else
                        {
                            invalidleadModels.Add(leadmodel);
                        }
                    }

                    uow.Complete();
                }
            }

            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    AsyncHelper.RunSync(() => ProcessImportLeadModelsResultAsync(args, invalidleadModels));
                }

                uow.Complete();
            }
        }

        private async Task CreateLeadMOdelsAsync(ImportLeadModelsDto input)
        {
            var tenantId = CurrentUnitOfWork.GetTenantId();

            if (tenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(tenantId.Value);
            }

            var leadModel = _objectMapper.Map<LeadModel>(input); //Passwords is not mapped (see mapping configuration)

            
            await _leadModelsRepository.InsertAsync(leadModel);
        }

        private async Task UpdateLeadModelAsync(ImportLeadModelsDto input)
        {
            var tenantId = CurrentUnitOfWork.GetTenantId();

            if (tenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(tenantId.Value);
            }

            
            var LeadModels = _leadModelsRepository.GetAll().Where(w => w.Name == input.Name && w.Description == input.Description);

            if (LeadModels.Count() == 0)
            {
                CreateLeadMOdelsAsync(input);
            }
            else
            {

                foreach (var LeadModel in LeadModels)
                {
                    _objectMapper.Map(input, LeadModel);
                }
            }

        }

        private async Task ProcessImportLeadModelsResultAsync(ImportLeadModelsFromExcelJobArgs args,
            List<ImportLeadModelsDto> invalidleadModels)
        {
            if (invalidleadModels.Any())
            {
                var file = _invalidLeadModelExporter.ExportToFile(invalidleadModels);
                await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    new LocalizableString("AllLeadModelssSuccessfullyImportedFromExcel", RMACTConsts.LocalizationSourceName),
                    null,
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        public void SendInvalidExcelNotification(ImportLeadModelsFromExcelJobArgs args)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                        args.User,
                        new LocalizableString("FileCantBeConvertedToLeadModelList", RMACTConsts.LocalizationSourceName),
                        null,
                        Abp.Notifications.NotificationSeverity.Warn));
                }
                uow.Complete();
            }
        }


    }
}
