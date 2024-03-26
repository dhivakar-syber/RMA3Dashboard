using System.Collections.Generic;
using SyberGate.RMACT.Authorization.Delegation;
using SyberGate.RMACT.Authorization.Users.Delegation.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.Layout
{
    public class ActiveUserDelegationsComboboxViewModel
    {
        public IUserDelegationConfiguration UserDelegationConfiguration { get; set; }
        
        public List<UserDelegationDto> UserDelegations { get; set; }
    }
}
