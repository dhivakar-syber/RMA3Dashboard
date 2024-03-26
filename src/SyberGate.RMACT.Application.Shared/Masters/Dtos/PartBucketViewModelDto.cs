
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class PartBucketViewModelDto:  EntityDto<int?>
    {
		[Required]
        [StringLength(PartBucketConsts.MaxRMSpecLength, MinimumLength = PartBucketConsts.MinRMSpecLength)]
        public string RMSpec { get; set; }

        public string RawMaterial { get; set; }

		public decimal Price { get; set; }

		[StringLength(PartBucketConsts.MaxBuyerLength, MinimumLength = PartBucketConsts.MinBuyerLength)]
		public string Buyer { get; set; }


		[StringLength(PartBucketConsts.MaxSupplierLength, MinimumLength = PartBucketConsts.MinSupplierLength)]
		public string Supplier { get; set; }

		public bool IsCurrentPrice { get; set; }

	}
}
