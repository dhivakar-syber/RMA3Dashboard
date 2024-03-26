using Abp.AutoMapper;
using SyberGate.RMACT.Sessions.Dto;

namespace SyberGate.RMACT.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}