using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.CommodityTrees
{
    public class CreateOrEditCommodityTreeModalViewModel
    {
       public CreateOrEditCommodityTreeDto CommodityTree { get; set; }

	   
       
	   public bool IsEditMode => CommodityTree.Id.HasValue;
    }
}