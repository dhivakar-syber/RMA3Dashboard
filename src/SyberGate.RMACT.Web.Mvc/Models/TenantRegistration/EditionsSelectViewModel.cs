using Abp.AutoMapper;
using SyberGate.RMACT.MultiTenancy.Dto;

namespace SyberGate.RMACT.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
