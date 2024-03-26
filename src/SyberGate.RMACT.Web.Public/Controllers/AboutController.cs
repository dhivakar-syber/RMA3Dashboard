using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Controllers;

namespace SyberGate.RMACT.Web.Public.Controllers
{
    public class AboutController : RMACTControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}