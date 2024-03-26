using Abp.AspNetCore.Mvc.Authorization;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Storage;
using Abp.BackgroundJobs;

namespace SyberGate.RMACT.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}