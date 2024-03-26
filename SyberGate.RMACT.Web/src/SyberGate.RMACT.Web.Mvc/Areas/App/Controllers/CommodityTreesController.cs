using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Web.Areas.App.Models.Common.Modals;
using SyberGate.RMACT.Web.Areas.App.Models.CommodityTrees;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Masters;


namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_CommodityTrees)]
    public class CommodityTreesController : RMACTControllerBase
    {
        private readonly IRepository<CommodityTree, long> _commodityTreeRepository;

        public CommodityTreesController(IRepository<CommodityTree, long> commodityTreeRepository)
        {
            _commodityTreeRepository = commodityTreeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CommodityTrees_ManageCommodityTree)]
        public PartialViewResult CreateModal(long? parentId)
        {
            return PartialView("_CreateModal", new CreateCommodityTreeModalViewModel(parentId));
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CommodityTrees_ManageCommodityTree)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var organizationUnit = await _commodityTreeRepository.GetAsync(id);
            var model = ObjectMapper.Map<EditCommodityTreeModalViewModel>(organizationUnit);

            return PartialView("_EditModal", model);
        }

    }
}