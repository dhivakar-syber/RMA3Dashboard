using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.IndexNames;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_IndexNames)]
    public class IndexNamesController : RMACTControllerBase
    {
        private readonly IIndexNamesAppService _indexNamesAppService;

        public IndexNamesController(IIndexNamesAppService indexNamesAppService)
        {
            _indexNamesAppService = indexNamesAppService;
        }

        public ActionResult Index()
        {
            var model = new IndexNamesViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_IndexNames_Create, AppPermissions.Pages_Administration_IndexNames_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetIndexNameForEditOutput getIndexNameForEditOutput;

				if (id.HasValue){
					getIndexNameForEditOutput = await _indexNamesAppService.GetIndexNameForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getIndexNameForEditOutput = new GetIndexNameForEditOutput{
						IndexName = new CreateOrEditIndexNameDto()
					};
				}

				var viewModel = new CreateOrEditIndexNameModalViewModel()
				{
					IndexName = getIndexNameForEditOutput.IndexName,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			

        public async Task<PartialViewResult> ViewIndexNameModal(int id)
        {
			var getIndexNameForViewDto = await _indexNamesAppService.GetIndexNameForView(id);

            var model = new IndexNameViewModel()
            {
                IndexName = getIndexNameForViewDto.IndexName
            };

            return PartialView("_ViewIndexNameModal", model);
        }


    }
}