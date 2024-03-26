using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.DashboardCustomization;
using System.Threading.Tasks;
using SyberGate.RMACT.Web.Areas.App.Startup;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class RMA3DashboardController : CustomizableDashboardControllerBase
    {
        public RMA3DashboardController(DashboardViewConfiguration dashboardViewConfiguration, 
            IDashboardCustomizationAppService dashboardCustomizationAppService) 
            : base(dashboardViewConfiguration, dashboardCustomizationAppService)
        {

        }

        public async Task<ActionResult> Index()
        {
            return await GetView(RMACTDashboardCustomizationConsts.DashboardNames.RMA3Dashboard);
        }
    }
}