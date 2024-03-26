using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dtos
{
	public class PartLeadModelMatrixDto: EntityDto
	{
		

		public string PartNo { get; set; }

		public string PartDespn { get; set; }

		public int LeadModelId { get; set; }


		public int Quantity { get; set; } 

	}
}
