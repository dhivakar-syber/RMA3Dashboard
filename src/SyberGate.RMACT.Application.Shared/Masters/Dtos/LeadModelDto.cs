
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class LeadModelDto : EntityDto
    {
		

		public int DocId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }



    }
}