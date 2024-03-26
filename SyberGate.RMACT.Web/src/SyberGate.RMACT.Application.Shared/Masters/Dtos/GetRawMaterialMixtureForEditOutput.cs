using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetRawMaterialMixtureForEditOutput
    {
		public CreateOrEditRawMaterialMixtureDto RawMaterialMixture { get; set; }

		public string RMGroupName { get; set;}

		public string RawMaterialGradeName { get; set;}

		public string BuyerName { get; set;}

		public string SupplierName { get; set;}


    }
}