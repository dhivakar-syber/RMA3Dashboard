
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class PartModelMatrixDto : EntityDto
    {
		public string PartNumber { get; set; }

		public string Name { get; set; }

		public int Quantity { get; set; }


		 public int LeadModelId { get; set; }

		 
    }
}