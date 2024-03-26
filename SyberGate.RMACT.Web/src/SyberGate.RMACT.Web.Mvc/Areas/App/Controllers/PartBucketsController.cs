using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.PartBuckets;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.BackgroundJobs;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using SyberGate.RMACT.Masters.Importing;
using SyberGate.RMACT.Storage;
using System.Linq;
using Abp.IO.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_PartBuckets)]
    public class PartBucketsController : RMACTControllerBase
    {
        private readonly IPartBucketsAppService _partBucketsAppService;
		protected readonly IBinaryObjectManager BinaryObjectManager;
		protected readonly IBackgroundJobManager BackgroundJobManager;

		public PartBucketsController(IPartBucketsAppService partBucketsAppService, IBinaryObjectManager binaryObjectManager,
			IBackgroundJobManager backgroundJobManager)
        {
            _partBucketsAppService = partBucketsAppService;
			BinaryObjectManager = binaryObjectManager;
			BackgroundJobManager = backgroundJobManager;
			
	}

        public ActionResult Index()
        {
            var model = new PartBucketsViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_PartBuckets_Create, AppPermissions.Pages_Administration_PartBuckets_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetPartBucketForEditOutput getPartBucketForEditOutput;

				if (id.HasValue){
					getPartBucketForEditOutput = await _partBucketsAppService.GetPartBucketForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getPartBucketForEditOutput = new GetPartBucketForEditOutput{
						PartBucket = new CreateOrEditPartBucketDto()
					};
				}

				var viewModel = new CreateOrEditPartBucketModalViewModel()
				{
					PartBucket = getPartBucketForEditOutput.PartBucket,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}


		public async Task<PartialViewResult> ViewPartBucketModal(int id)
        {
			var getPartBucketForViewDto = await _partBucketsAppService.GetPartBucketForView(id);

            var model = new PartBucketViewModel()
            {
                PartBucket = getPartBucketForViewDto.PartBucket
            };

            return PartialView("_ViewPartBucketModal", model);
        }

		[HttpPost]
		[AbpMvcAuthorize(AppPermissions.Pages_Administration_PartBuckets_Create)]
		public async Task<JsonResult> ImportFromExcel()
		{
			try
			{
				var file = Request.Form.Files.First();

				if (file == null)
				{
					throw new UserFriendlyException(L("File_Empty_Error"));
				}

				if (file.Length > 1048576 * 100) //100 MB
				{
					throw new UserFriendlyException(L("File_SizeLimit_Error"));
				}

				byte[] fileBytes;
				using (var stream = file.OpenReadStream())
				{
					fileBytes = stream.GetAllBytes();
				}

				var tenantId = AbpSession.TenantId;
				var fileObject = new BinaryObject(tenantId, fileBytes, $"{DateTime.UtcNow} import from excel file.");

				await BinaryObjectManager.SaveAsync(fileObject);

				await BackgroundJobManager.EnqueueAsync<ImportPartBucketDatasToExcelJob, ImportPartBucketFromJobArgs>(new ImportPartBucketFromJobArgs
				{
					TenantId = tenantId,
					BinaryObjectId = fileObject.Id,
					User = AbpSession.ToUserIdentifier()
				});


				return Json(new AjaxResponse(new { }));
			}
			catch (UserFriendlyException ex)
			{
				return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
			}
		}

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_PartBuckets_Create, AppPermissions.Pages_Administration_PartBuckets_Edit)]
        public async Task<PartialViewResult> PartBucketViewModalData(string buyerid, string supplierid, string rmspec, string rm, decimal price, bool type)
        {

            var rmProcessList = await _partBucketsAppService.GetPartBucketForProcess(new PartBucketViewModelDto { Buyer = buyerid, Supplier = supplierid, RMSpec = rmspec });
            var model = new PartBucketViewModalDetail
            {
                BuyerId = buyerid,
                SupplierId = supplierid,
                PartNo = rmspec,
                RawMaterial = rm,
                Price = price,                
                IsCurrentPrice = type,
                RMBuckets = rmProcessList
            };

            return PartialView("_PartBucketViewModal", model);
        }


	}
}