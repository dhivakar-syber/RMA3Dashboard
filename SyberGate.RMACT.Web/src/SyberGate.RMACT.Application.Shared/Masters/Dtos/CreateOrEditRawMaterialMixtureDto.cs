
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditRawMaterialMixtureDto : EntityDto<int?>
    {

		[Range(RawMaterialMixtureConsts.MinWeightRatioValue, RawMaterialMixtureConsts.MaxWeightRatioValue)]
		public decimal WeightRatio { get; set; }
		
		
		[Range(RawMaterialMixtureConsts.MinLossRatioValue, RawMaterialMixtureConsts.MaxLossRatioValue)]
		public decimal? LossRatio { get; set; }
		
		
		 public int RMGroupId { get; set; }
		 
		 		 public int RawMaterialGradeId { get; set; }
		 
		 		 public int BuyerId { get; set; }
		 
		 		 public int SupplierId { get; set; }
		 
		 
    }
}