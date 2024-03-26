using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllBaseRMRatesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string RMGroupNameFilter { get; set; }

		 		 public string UnitOfMeasurementCodeFilter { get; set; }

		 		 public string YearNameFilter { get; set; }

		 		 public string BuyerNameFilter { get; set; }

		 		 public string SupplierNameFilter { get; set; }

		 
    }
}