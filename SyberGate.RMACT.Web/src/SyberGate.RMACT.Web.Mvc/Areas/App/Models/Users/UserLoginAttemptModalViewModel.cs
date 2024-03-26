using System.Collections.Generic;
using SyberGate.RMACT.Authorization.Users.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}