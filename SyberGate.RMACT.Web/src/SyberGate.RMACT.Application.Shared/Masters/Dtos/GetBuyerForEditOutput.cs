using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetBuyerForEditOutput
    {
		public CreateOrEditBuyerDto Buyer { get; set; }

		public string UserName { get; set;}


    }
}