using Abp.AutoMapper;
using SyberGate.RMACT.Masters;

namespace SyberGate.RMACT.Web.Areas.App.Models.CommodityTrees
{
    [AutoMapFrom(typeof(CommodityTree))]
    public class EditCommodityTreeModalViewModel
    {
        public long? Id { get; set; }

        public string DisplayName { get; set; }

        public bool IsLeaf { get; set; }
    }
}