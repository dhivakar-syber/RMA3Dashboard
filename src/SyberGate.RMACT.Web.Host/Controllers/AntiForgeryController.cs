using Microsoft.AspNetCore.Antiforgery;

namespace SyberGate.RMACT.Web.Controllers
{
    public class AntiForgeryController : RMACTControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
