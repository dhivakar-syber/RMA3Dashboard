using System.Collections.Generic;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Editions.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}