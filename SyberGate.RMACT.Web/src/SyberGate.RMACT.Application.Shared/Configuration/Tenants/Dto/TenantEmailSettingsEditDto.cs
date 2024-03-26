using Abp.Auditing;
using SyberGate.RMACT.Configuration.Dto;

namespace SyberGate.RMACT.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}