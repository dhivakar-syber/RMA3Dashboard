namespace SyberGate.RMACT.Web.Areas.App.Models.CommodityTrees
{
    public class CreateCommodityTreeModalViewModel
    {
        public long? ParentId { get; set; }
        
        public CreateCommodityTreeModalViewModel(long? parentId)
        {
            ParentId = parentId;
        }
    }
}