using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.A3Documents;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using SyberGate.RMACT.Web.Areas.App.Models.SupportAttachments;
using Microsoft.AspNetCore.Http;
using System.IO;
using Abp.Domain.Repositories;
using System.Linq;
using Abp.IO.Extensions;
using Abp.UI;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_A3Documents)]
    public class A3DocumentsController : RMACTControllerBase
    {
        private readonly IA3DocumentsAppService _a3DocumentsAppService;
        
        public A3DocumentsController(IA3DocumentsAppService a3DocumentsAppService)
        {
            _a3DocumentsAppService = a3DocumentsAppService;
           
        }

        public ActionResult Index()
        {
            var model = new A3DocumentsViewModel
			{
				FilterText = ""
			};

            return View(model);
        }
		
		public ActionResult ListA3Documents(string buyer ,string supplier)
		{
			var model = new A3DocumentsViewModel
			{
				FilterText = "",
				BuyerName = buyer,
                SupplierName=supplier
			};

			return PartialView("_ListA3Documents", model);
		}

        public ActionResult ListSupportAttachment(int a3Id,string buyer,string supplier,string version)
        {
            var model = new SupportAttachmentsViewModel
            {
                FilterText = "",
                A3Id = a3Id,
                BuyerName = buyer,
                suppliername= supplier,
                version=version
                
                
            };

            return View("_supportAttachments", model);
        }


        [HttpPost]
        public async Task UploadExcel(int a3Id, string buyer, string supplier, string version)
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

            SupportAttachmentsDto insert=new SupportAttachmentsDto();

            insert.A3Id = a3Id;
            insert.FileName= file.FileName;
            insert.Filebyte= fileBytes;
            insert.Buyer = buyer;
            insert.Supplier = supplier;
            insert.Version = version;

            await _a3DocumentsAppService.uploadexcel(insert);
            

        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_A3Documents_Create, AppPermissions.Pages_Administration_A3Documents_Edit)]
			public async Task<ActionResult> CreateOrEdit(int? id)
			{
				GetA3DocumentForEditOutput getA3DocumentForEditOutput;

				if (id.HasValue){
					getA3DocumentForEditOutput = await _a3DocumentsAppService.GetA3DocumentForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getA3DocumentForEditOutput = new GetA3DocumentForEditOutput{
						A3Document = new CreateOrEditA3DocumentDto()
					};
				}

				var viewModel = new CreateOrEditA3DocumentViewModel()
				{
					A3Document = getA3DocumentForEditOutput.A3Document,                
				};

				return View(viewModel);
			}
			



    }
}