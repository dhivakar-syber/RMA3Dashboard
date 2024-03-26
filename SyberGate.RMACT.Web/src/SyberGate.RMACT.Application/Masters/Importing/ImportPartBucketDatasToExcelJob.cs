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

	public class ImportPartBucketDatasToExcelJob : BackgroundJob<ImportPartBucketFromJobArgs>, ITransientDependency
	{
		private readonly IRepository<RMTapTool> _partBucketRepository;
		private readonly IPartBucketExcelDataReader _PartBucketExcelDataReader;
		private readonly IInvalidPartBucketExporter _invalidPartBucketExporter;
		private readonly IAppNotifier _appNotifier;
		private readonly IUserPolicy _userPolicy;
		private readonly IBinaryObjectManager _binaryObjectManager;
		private readonly IObjectMapper _objectMapper;
		private readonly IUnitOfWorkManager _unitOfWorkManager;
		private readonly IRepository<Supplier> _supplierRepository;
        private readonly IRepository<Buyer> _buyerRepository;
        public ImportPartBucketDatasToExcelJob(

			IPartBucketExcelDataReader PartBucketExcelDataReader,
			IInvalidPartBucketExporter InvalidPartBucketExporter,
			IAppNotifier appNotifier,
			IUserPolicy userPolicy,
			IBinaryObjectManager binaryObjectManager,
			IObjectMapper objectMapper,
			IUnitOfWorkManager unitOfWorkManager,
			IRepository<RMTapTool> partBucketRepository,
            IRepository<Supplier> SupplierRepository,
            IRepository<Buyer> BuyerRepository)
		{
			_PartBucketExcelDataReader = PartBucketExcelDataReader;
			_invalidPartBucketExporter = InvalidPartBucketExporter;
			_appNotifier = appNotifier;
			_userPolicy = userPolicy;
			_binaryObjectManager = binaryObjectManager;
			_objectMapper = objectMapper;
			_unitOfWorkManager = unitOfWorkManager;
			_partBucketRepository = partBucketRepository;
            _supplierRepository = SupplierRepository;
            _buyerRepository = BuyerRepository;

        }

		public override void Execute(ImportPartBucketFromJobArgs args)
		{
			var PartBuckets = GetPartBucketsFromExcelOrNull(args);
			if (PartBuckets == null || !PartBuckets.Any())
			{
				SendInvalidExcelNotification(args);
				return;
			}

			CreatePartBuckets(args, PartBuckets);
		}





		public List<ImportPartBucketDto> GetPartBucketsFromExcelOrNull(ImportPartBucketFromJobArgs args)
		{
			using (var uow = _unitOfWorkManager.Begin())
			{
				using (CurrentUnitOfWork.SetTenantId(args.TenantId))
				{
					try
					{
						if (args.Bytes != null)
						{
							return _PartBucketExcelDataReader.GetPartBucketFromExcel(args.Bytes);
						}
						else
						{
							var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
							return _PartBucketExcelDataReader.GetPartBucketFromExcel(file.Bytes);
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

		public void CreatePartBuckets(ImportPartBucketFromJobArgs args, List<ImportPartBucketDto> PartBuckets)
		{
			var invalidPartBucket = new List<ImportPartBucketDto>();


			foreach (var partBucket in PartBuckets)
			{
				using (var uow = _unitOfWorkManager.Begin())
				{
					using (CurrentUnitOfWork.SetTenantId(args.TenantId))
					{
						if (partBucket.CanBeImported())
						{
							try
							{
								AsyncHelper.RunSync(() => CreatePartBucketAsync(partBucket));
							}
							catch (UserFriendlyException exception)
							{
								partBucket.Exception = exception.Message;
								invalidPartBucket.Add(partBucket);
							}
							catch (Exception exception)
							{
								partBucket.Exception = exception.ToString();
								invalidPartBucket.Add(partBucket);
							}
						}
						else
						{
							invalidPartBucket.Add(partBucket);
						}
					}

					uow.Complete();
				}
			}

			using (var uow = _unitOfWorkManager.Begin())
			{
				using (CurrentUnitOfWork.SetTenantId(args.TenantId))
				{
					AsyncHelper.RunSync(() => ProcessImportPartBucketsResultAsync(args, invalidPartBucket));
				}

				uow.Complete();
			}
		}

		private async Task CreatePartBucketAsync(ImportPartBucketDto input)
		{
			var tenantId = CurrentUnitOfWork.GetTenantId();

			if (tenantId.HasValue)
			{
				await _userPolicy.CheckMaxUserCountAsync(tenantId.Value);
			}
		

			var PartBucket = _objectMapper.Map<RMTapTool>(input); //Passwords is not mapped (see mapping configuration)

            //PartBucket.CreatedOn= DateTime.Now;
            string[] supplierstring = input.Supplier.Split('-');
			var suppliercode= supplierstring[supplierstring.Length - 1].Trim();
			
			var Supplier = _supplierRepository.GetAll().Where(w => w.Code == suppliercode);
			PartBucket.SupplierId = Supplier.FirstOrDefault().Id;

			var buyer = _buyerRepository.GetAll().Where(w => w.Name == input.Buyer);

			PartBucket.BuyerId= buyer.FirstOrDefault().Id;
			PartBucket.RMGrade = "";
			//PartBucket.RMSpec = "";
            await _partBucketRepository.InsertAsync(PartBucket);
		}


		//private async Task UpdatePartBucketAsync(ImportPartBucketDto input)
		//{
		//	var tenantId = CurrentUnitOfWork.GetTenantId();

		//	if (tenantId.HasValue)
		//	{
		//		await _userPolicy.CheckMaxUserCountAsync(tenantId.Value);
		//	}



		//	//var PartBuckets = _partBucketRepository.GetAll().Where(w => w.PartNumber == input.PartNumber && w.Buckets == input.Buckets && w.Buyer == input.Buyer && w.Supplier == input.Supplier && w.Month==input.Month && w.Year==input.Year);

            
  //              	CreatePartBucketAsync(input);
           

  //              //if(PartBuckets.Count() == 0)
  //              //{

  //              //	CreatePartBucketAsync(input);
  //              //}
  //              //else
  //              //{


  //              //	foreach (var PartBucket in PartBuckets)
  //              //	{
  //              //		//PartBucket.CreatedOn= DateTime.Now;

  //              //		_objectMapper.Map(input, PartBucket);
  //              //	}



  //              //}

  //      }

        private async Task ProcessImportPartBucketsResultAsync(ImportPartBucketFromJobArgs args,
			List<ImportPartBucketDto> invalidPartBucket)
		{
			if (invalidPartBucket.Any())
			{
				var file = _invalidPartBucketExporter.ExportToFile(invalidPartBucket);
				await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
			}
			else
			{
				await _appNotifier.SendMessageAsync(
					args.User,
					new LocalizableString("AllPartBucketSuccessfullyImportedFromExcel", RMACTConsts.LocalizationSourceName),
					null,
					Abp.Notifications.NotificationSeverity.Success);
			}
		}

		public void SendInvalidExcelNotification(ImportPartBucketFromJobArgs args)
		{
			using (var uow = _unitOfWorkManager.Begin())
			{
				using (CurrentUnitOfWork.SetTenantId(args.TenantId))
				{
					AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
						args.User,
						new LocalizableString("FileCantBeConvertedToPartBucketList", RMACTConsts.LocalizationSourceName),
						null,
						Abp.Notifications.NotificationSeverity.Warn));
				}
				uow.Complete();
			}
		}


	}
}

