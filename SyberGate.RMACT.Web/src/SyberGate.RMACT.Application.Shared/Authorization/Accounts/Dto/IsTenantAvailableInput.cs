using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;

namespace SyberGate.RMACT.Authorization.Accounts.Dto
{
    public class IsTenantAvailableInput
    {
        [Required]
        [MaxLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }
    }
}