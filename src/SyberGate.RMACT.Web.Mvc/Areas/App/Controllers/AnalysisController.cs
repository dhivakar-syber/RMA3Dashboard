using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Controllers;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    public class AnalysisController : RMACTControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
