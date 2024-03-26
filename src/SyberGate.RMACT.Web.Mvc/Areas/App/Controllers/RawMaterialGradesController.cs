using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.RawMaterialGrades;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialGrades)]
    public class RawMaterialGradesController : RMACTControllerBase
    {
        private readonly IRawMaterialGradesAppService _rawMaterialGradesAppService;

        public RawMaterialGradesController(IRawMaterialGradesAppService rawMaterialGradesAppService)
        {
            _rawMaterialGradesAppService = rawMaterialGradesAppService;
        }

        public ActionResult Index()
        {
            var model = new RawMaterialGradesViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialGrades_Create, AppPermissions.Pages_Administration_RawMaterialGrades_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetRawMaterialGradeForEditOutput getRawMaterialGradeForEditOutput;

				if (id.HasValue){
					getRawMaterialGradeForEditOutput = await _rawMaterialGradesAppService.GetRawMaterialGradeForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getRawMaterialGradeForEditOutput = new GetRawMaterialGradeForEditOutput{
						RawMaterialGrade = new CreateOrEditRawMaterialGradeDto()
					};
				}

				var viewModel = new CreateOrEditRawMaterialGradeModalViewModel()
				{
					RawMaterialGrade = getRawMaterialGradeForEditOutput.RawMaterialGrade,
					RawMaterialGradeName = getRawMaterialGradeForEditOutput.RawMaterialGradeName,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			


        [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialGrades_Create, AppPermissions.Pages_Administration_RawMaterialGrades_Edit)]
        public PartialViewResult RawMaterialGradeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new RawMaterialGradeRawMaterialGradeLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_RawMaterialGradeRawMaterialGradeLookupTableModal", viewModel);
        }

    }
}