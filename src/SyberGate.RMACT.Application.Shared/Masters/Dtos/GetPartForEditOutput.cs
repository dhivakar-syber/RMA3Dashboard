using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetPartForEditOutput
    {
		public CreateOrEditPartDto Part { get; set; }

		public string SupplierName { get; set;}

		public string BuyerName { get; set;}

		public string RMGroupName { get; set;}


    }
}