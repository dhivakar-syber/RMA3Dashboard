using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllA3DocumentsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string BuyerFilter { get; set; }

		public string SupplierFilter { get; set; }

		public string MonthFilter { get; set; }

		public string YearFilter { get; set; }

		public bool IsConfirmedFilter { get; set; }


	}
}