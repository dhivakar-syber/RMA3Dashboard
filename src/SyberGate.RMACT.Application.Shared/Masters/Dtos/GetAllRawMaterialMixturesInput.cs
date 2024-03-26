using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllRawMaterialMixturesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string RMGroupNameFilter { get; set; }

		 		 public string RawMaterialGradeNameFilter { get; set; }

		 		 public string BuyerNameFilter { get; set; }

		 		 public string SupplierNameFilter { get; set; }

		 
    }
}