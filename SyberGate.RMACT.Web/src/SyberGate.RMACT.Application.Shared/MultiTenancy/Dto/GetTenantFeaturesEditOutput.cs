using System.Collections.Generic;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Editions.Dto;

namespace SyberGate.RMACT.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}