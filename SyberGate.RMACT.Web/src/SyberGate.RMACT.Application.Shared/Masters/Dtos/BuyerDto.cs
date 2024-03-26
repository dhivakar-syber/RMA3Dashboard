
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class BuyerDto : EntityDto
    {
		public string Code { get; set; }

		public string Name { get; set; }


		 public long? UserId { get; set; }
		public string Department { get; set; }

		 
    }
}