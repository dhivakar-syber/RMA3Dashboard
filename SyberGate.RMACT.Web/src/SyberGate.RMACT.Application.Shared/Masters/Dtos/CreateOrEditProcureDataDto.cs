
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditProcureDataDto : EntityDto<int?>
    {

		[StringLength(ProcureDataConsts.MaxPartNoLength, MinimumLength = ProcureDataConsts.MinPartNoLength)]
		public string PartNo { get; set; }
		
		
		[Required]
		[StringLength(ProcureDataConsts.MaxDescriptionLength, MinimumLength = ProcureDataConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		
		public string SupplierCode { get; set; }
		
		
		public string SuppliserName { get; set; }
		
		
		[Required]
		public decimal CurrentExwPrice { get; set; }
		
		
		public string PriceCurrency { get; set; }
		
		
		public string Uom { get; set; }
		
		
		public string Buyer { get; set; }
		
		
		[Required]
		public DateTime FromDate { get; set; }
		
		
		[Required]
		public DateTime ToDate { get; set; }
		
		
		[Required]
		public decimal PackagingCost { get; set; }
		
		
		[Required]
		public decimal LogisticsCost { get; set; }
		
		
		public string PlantCode { get; set; }
		
		
		public string PlantDescription { get; set; }
		
		
		public string ContractNo { get; set; }
		
		
		[Required]
		public decimal SOB { get; set; }
		
		
		[Required]
		public decimal EPU { get; set; }
		
		
		public string Status { get; set; }
		
		
		[Required]
		public DateTime CreationTime { get; set; }
		
		
		public DateTime? DeletionTime { get; set; }
		
		
		[Required]
		public bool IsDeleted { get; set; }
		
		
		public DateTime? LastModificationTime { get; set; }
		
		

    }
}