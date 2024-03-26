using Abp.AutoMapper;
using SyberGate.RMACT.MultiTenancy;
using SyberGate.RMACT.MultiTenancy.Dto;
using SyberGate.RMACT.Web.Areas.App.Models.Common;

namespace SyberGate.RMACT.Web.Areas.App.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}