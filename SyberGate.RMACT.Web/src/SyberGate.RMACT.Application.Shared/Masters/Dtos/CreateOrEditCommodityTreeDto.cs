
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditCommodityTreeDto : EntityDto<int?>
    {

		[Required]
		[StringLength(CommodityTreeConsts.MaxCodeLength, MinimumLength = CommodityTreeConsts.MinCodeLength)]
		public string Code { get; set; }
		
		
		[Required]
		[StringLength(CommodityTreeConsts.MaxDispalayNameLength, MinimumLength = CommodityTreeConsts.MinDispalayNameLength)]
		public string DispalayName { get; set; }
		
		
		[Range(CommodityTreeConsts.MinParentIdValue, CommodityTreeConsts.MaxParentIdValue)]
		public long? ParentId { get; set; }
		
		

    }
}