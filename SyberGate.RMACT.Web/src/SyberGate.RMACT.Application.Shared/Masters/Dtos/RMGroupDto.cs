
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class RMGroupDto : EntityDto
    {
		public string Name { get; set; }

		public bool HasMixture { get; set; }



    }
}