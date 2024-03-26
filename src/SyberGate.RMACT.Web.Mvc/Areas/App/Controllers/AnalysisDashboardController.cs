using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.DashboardCustomization;
using SyberGate.RMACT.Web.Areas.App.Startup;
using SyberGate.RMACT.Web.Controllers;
using System.Threading.Tasks;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Analysis_Dashboard)]
    public class AnalysisDashboardController : CustomizableDashboardControllerBase
    {
        public AnalysisDashboardController(DashboardViewConfiguration dashboardViewConfiguration,
             IDashboardCustomizationAppService dashboardCustomizationAppService)
             : base(dashboardViewConfiguration, dashboardCustomizationAppService)
        {

        }

        public async Task<ActionResult> Index()
        {
            return await GetView(RMACTDashboardCustomizationConsts.DashboardNames.AnalysisDashboard); //DefaultHostDashboard AnalysisDashboard
        }
    }
}
