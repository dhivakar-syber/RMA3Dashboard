using Abp.AutoMapper;
using SyberGate.RMACT.MultiTenancy.Dto;

namespace SyberGate.RMACT.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}