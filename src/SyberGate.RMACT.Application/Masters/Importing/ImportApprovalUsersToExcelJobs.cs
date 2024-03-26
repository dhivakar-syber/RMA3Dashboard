
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
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SyberGate.RMACT.Masters.Importing
{

    public class ImportApprovalUsersToExcelJobs : BackgroundJob<ImportApprovalUsersFromJobArgs>, ITransientDependency
    {
        
        
        private readonly IApprovalUsersExcelDataReader _approvalUsersExcelDataReader;
        private readonly IInvalidApprovalUsersExporter _invalidApprovalUsersExporter;
        private readonly IAppNotifier _appNotifier;
        private readonly IUserPolicy _userPolicy;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<ApprovalUser> _approvalUserRepository;

        public ImportApprovalUsersToExcelJobs(
            
            IApprovalUsersExcelDataReader approvalUsersExcelDataReader,
            IInvalidApprovalUsersExporter IInvalidApprovalUsersExporter,
            IAppNotifier appNotifier,
            IUserPolicy userPolicy,
            IBinaryObjectManager binaryObjectManager,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<ApprovalUser> ApprovalUserRepository)
        {
            _approvalUserRepository= ApprovalUserRepository;
            _approvalUsersExcelDataReader = approvalUsersExcelDataReader;
            _invalidApprovalUsersExporter = IInvalidApprovalUsersExporter;
            _appNotifier = appNotifier;
            _userPolicy = userPolicy;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            
        }
      
        public override void Execute(ImportApprovalUsersFromJobArgs args)
        {
            var ApprovalUsers = GetApprovaluserFromExcelOrNull(args);
            if (ApprovalUsers == null || !ApprovalUsers.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            CreateApprovalUsers(args, ApprovalUsers);
        }





        public List<ImportApprovalUsersDto> GetApprovaluserFromExcelOrNull(ImportApprovalUsersFromJobArgs args)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    try
                    {
                        if (args.Bytes != null)
                        {
                            return _approvalUsersExcelDataReader.GetApprovalUserFromExcel(args.Bytes);
                        }
                        else
                        {
                            var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                            return _approvalUsersExcelDataReader.GetApprovalUserFromExcel(file.Bytes);
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

        public void CreateApprovalUsers(ImportApprovalUsersFromJobArgs args, List<ImportApprovalUsersDto> ApprovalUsers)
        {
            var invalidApprovalUsers = new List<ImportApprovalUsersDto>();


            foreach (var ApprovalUser in ApprovalUsers)
            {
                using (var uow = _unitOfWorkManager.Begin())
                {
                    using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                    {
                        if (ApprovalUser.CanBeImported())
                        {
                            try
                            {
                                AsyncHelper.RunSync(() => UpdateApprovalUserAsync(ApprovalUser));
                            }
                            catch (UserFriendlyException exception)
                            {
                                ApprovalUser.Exception = exception.Message;
                                invalidApprovalUsers.Add(ApprovalUser);
                            }
                            catch (Exception exception)
                            {
                                ApprovalUser.Exception = exception.ToString();
                                invalidApprovalUsers.Add(ApprovalUser);
                            }
                        }
                        else
                        {
                            invalidApprovalUsers.Add(ApprovalUser);
                        }
                    }

                    uow.Complete();
                }
            }

            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    AsyncHelper.RunSync(() => ProcessImportapprovalusersResultAsync(args, invalidApprovalUsers));
                }

                uow.Complete();
            }
        }

        private async Task CreateApprovalUserAsync(ImportApprovalUsersDto input)
        {
            var tenantId = CurrentUnitOfWork.GetTenantId();

            if (tenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(tenantId.Value);
            }
            
            var approvaluser = _objectMapper.Map<ApprovalUser>(input); //Passwords is not mapped (see mapping configuration)


            await _approvalUserRepository.InsertAsync(approvaluser);
        }

        private async Task UpdateApprovalUserAsync(ImportApprovalUsersDto input)
        {
            var tenantId = CurrentUnitOfWork.GetTenantId();

            if (tenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(tenantId.Value);
            }

            var approvalusers = _approvalUserRepository.GetAll().Where(w => w.UserName == input.UserName );


            

            if (approvalusers.Count() == 0)

            {

                CreateApprovalUserAsync(input);
            }
            else
            {
                foreach (var approvaluser in approvalusers)
                {
                    
                    _objectMapper.Map(input, approvaluser);
                }
            }

        }

        private async Task ProcessImportapprovalusersResultAsync(ImportApprovalUsersFromJobArgs args,
            List<ImportApprovalUsersDto> invalidapprovalusers)
        {
            if (invalidapprovalusers.Any())
            {
                var file = _invalidApprovalUsersExporter.ExportToFile(invalidapprovalusers);
                await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    new LocalizableString("AllApprovalUsersSuccessfullyImportedFromExcel", RMACTConsts.LocalizationSourceName),
                    null,
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        public void SendInvalidExcelNotification(ImportApprovalUsersFromJobArgs args)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                using (CurrentUnitOfWork.SetTenantId(args.TenantId))
                {
                    AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                        args.User,
                        new LocalizableString("FileCantBeConvertedToapprovalUserlist", RMACTConsts.LocalizationSourceName),
                        null,
                        Abp.Notifications.NotificationSeverity.Warn));
                }
                uow.Complete();
            }
        }


    }
}

