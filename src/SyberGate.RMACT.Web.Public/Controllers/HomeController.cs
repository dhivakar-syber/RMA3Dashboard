using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Controllers;

namespace SyberGate.RMACT.Web.Public.Controllers
{
    public class HomeController : RMACTControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}