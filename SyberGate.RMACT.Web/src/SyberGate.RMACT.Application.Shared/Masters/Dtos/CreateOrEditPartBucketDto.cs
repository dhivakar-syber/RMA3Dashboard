
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditPartBucketDto : EntityDto<int?>
    {

		[Required]
        [StringLength(PartBucketConsts.MaxRMSpecLength, MinimumLength = PartBucketConsts.MinRMSpecLength)]
        public string RMSpec { get; set; }


        [Required]
		[StringLength(PartBucketConsts.MaxBucketsLength, MinimumLength = PartBucketConsts.MinBucketsLength)]
		public string Buckets { get; set; }
		
		
		[Range(PartBucketConsts.MinValueValue, PartBucketConsts.MaxValueValue)]
		public decimal Value { get; set; }
		
		
		[StringLength(PartBucketConsts.MaxBuyerLength, MinimumLength = PartBucketConsts.MinBuyerLength)]
		public string Buyer { get; set; }
		
		
		[StringLength(PartBucketConsts.MaxSupplierLength, MinimumLength = PartBucketConsts.MinSupplierLength)]
		public string Supplier { get; set; }
		

		public DateTime CreatedOn { get; set; }
		

    }
}