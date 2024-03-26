using Abp.AutoMapper;
using SyberGate.RMACT.Authorization.Users;
using SyberGate.RMACT.Authorization.Users.Dto;
using SyberGate.RMACT.Web.Areas.App.Models.Common;

namespace SyberGate.RMACT.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}