
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class RawMaterialMixtureDto : EntityDto
    {
		public decimal? WeightRatio { get; set; }

		public decimal? LossRatio { get; set; }


		 public int RMGroupId { get; set; }

		 		 public int RawMaterialGradeId { get; set; }

		 		 public int BuyerId { get; set; }

		 		 public int SupplierId { get; set; }

		public string RMGroupName { get; set; }
		public string BuyerName { get; set; }
		public string SupplierName { get; set; }
		 
    }
}