using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetBaseRMRateForEditOutput
    {
		public CreateOrEditBaseRMRateDto BaseRMRate { get; set; }

		public string RMGroupName { get; set;}

		public string UnitOfMeasurementCode { get; set;}

		public string YearName { get; set;}

		public string BuyerName { get; set;}

		public string SupplierName { get; set;}

		public DateTime SettledDate { get; set;}


    }
}