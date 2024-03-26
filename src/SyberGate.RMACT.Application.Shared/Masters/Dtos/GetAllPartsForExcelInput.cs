using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllPartsForExcelInput
    {
		public string Filter { get; set; }

		public string PartNoFilter { get; set; }


		 public string SupplierNameFilter { get; set; }

		 		 public string BuyerNameFilter { get; set; }

		 		 public string RMGroupNameFilter { get; set; }
		
		public string TemplatePath { get; set; }

		public int SupplierId { get; set; }
		public int BuyerId { get; set; }
	}
}