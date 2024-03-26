using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllProcureDatasInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string PartNoFilter { get; set; }

		public string DescriptionFilter { get; set; }

		public string SupplierCodeFilter { get; set; }

		public string SuppliserNameFilter { get; set; }

		public decimal? MaxCurrentExwPriceFilter { get; set; }
		public decimal? MinCurrentExwPriceFilter { get; set; }

		public string PriceCurrencyFilter { get; set; }

		public string UomFilter { get; set; }

		public string BuyerFilter { get; set; }

		public DateTime? MaxFromDateFilter { get; set; }
		public DateTime? MinFromDateFilter { get; set; }

		public DateTime? MaxToDateFilter { get; set; }
		public DateTime? MinToDateFilter { get; set; }

		public decimal? MaxPackagingCostFilter { get; set; }
		public decimal? MinPackagingCostFilter { get; set; }

		public decimal? MaxLogisticsCostFilter { get; set; }
		public decimal? MinLogisticsCostFilter { get; set; }

		public string PlantCodeFilter { get; set; }

		public string PlantDescriptionFilter { get; set; }

		public string ContractNoFilter { get; set; }

		public decimal? MaxSOBFilter { get; set; }
		public decimal? MinSOBFilter { get; set; }

		public decimal? MaxEPUFilter { get; set; }
		public decimal? MinEPUFilter { get; set; }

		public string StatusFilter { get; set; }

		public DateTime? MaxCreationTimeFilter { get; set; }
		public DateTime? MinCreationTimeFilter { get; set; }

		public DateTime? MaxDeletionTimeFilter { get; set; }
		public DateTime? MinDeletionTimeFilter { get; set; }

		public int IsDeletedFilter { get; set; }

		public DateTime? MaxLastModificationTimeFilter { get; set; }
		public DateTime? MinLastModificationTimeFilter { get; set; }



    }
}