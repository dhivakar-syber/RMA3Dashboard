using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllPartBucketsForExcelInput
    {
		public string Filter { get; set; }

		public string PartNumberFilter { get; set; }

		public string BucketsFilter { get; set; }

		public decimal? MaxValueFilter { get; set; }
		public decimal? MinValueFilter { get; set; }

		public string BuyerFilter { get; set; }

		public string SupplierFilter { get; set; }



    }
}