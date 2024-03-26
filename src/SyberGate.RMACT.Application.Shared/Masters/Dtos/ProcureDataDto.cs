
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class ProcureDataDto : EntityDto
    {
		public string PartNo { get; set; }

		public string Description { get; set; }

		public string SupplierCode { get; set; }

		public string SuppliserName { get; set; }

		public decimal CurrentExwPrice { get; set; }

		public string PriceCurrency { get; set; }

		public string Uom { get; set; }

		public string Buyer { get; set; }

		public DateTime FromDate { get; set; }

		public DateTime ToDate { get; set; }

		public decimal PackagingCost { get; set; }

		public decimal LogisticsCost { get; set; }

		public string PlantCode { get; set; }

		public string PlantDescription { get; set; }

		public string ContractNo { get; set; }

		public decimal SOB { get; set; }

		public decimal EPU { get; set; }

		public string Status { get; set; }

		public DateTime CreationTime { get; set; }

		public DateTime? DeletionTime { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? LastModificationTime { get; set; }



    }
}