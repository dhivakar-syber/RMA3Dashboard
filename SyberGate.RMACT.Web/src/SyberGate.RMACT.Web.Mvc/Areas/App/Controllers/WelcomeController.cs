using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Controllers;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class WelcomeController : RMACTControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}