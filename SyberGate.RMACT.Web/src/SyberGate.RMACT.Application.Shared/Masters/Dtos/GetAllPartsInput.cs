using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllPartsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string PartNoFilter { get; set; }


		 public string SupplierNameFilter { get; set; }

		 		 public string BuyerNameFilter { get; set; }

		 		 public string RMGroupNameFilter { get; set; }
    }
}