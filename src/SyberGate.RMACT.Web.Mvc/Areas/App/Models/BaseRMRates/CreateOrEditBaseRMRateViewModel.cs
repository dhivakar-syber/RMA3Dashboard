using SyberGate.RMACT.Masters.Dtos;
using System.Collections.Generic;
using System.Collections.Generic;

using Abp.Extensions;
using System;

namespace SyberGate.RMACT.Web.Areas.App.Models.BaseRMRates
{
    public class CreateOrEditBaseRMRateModalViewModel
    {
       public CreateOrEditBaseRMRateDto BaseRMRate { get; set; }

	   		public string RMGroupName { get; set;}

		public string UnitOfMeasurementCode { get; set;}

		public string YearName { get; set;}

		public string BuyerName { get; set;}

		public string SupplierName { get; set;}


       public List<BaseRMRateUnitOfMeasurementLookupTableDto> BaseRMRateUnitOfMeasurementList { get; set;}

public List<BaseRMRateYearLookupTableDto> BaseRMRateYearList { get; set;}


	   public bool IsEditMode => BaseRMRate.Id.HasValue;

		public bool IsRevision { get; set; }

		public DateTime SettledDate { get; set; }
    }
}