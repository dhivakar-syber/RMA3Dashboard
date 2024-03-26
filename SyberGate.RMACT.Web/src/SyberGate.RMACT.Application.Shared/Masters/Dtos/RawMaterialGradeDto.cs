
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class RawMaterialGradeDto : EntityDto
    {
		public string Name { get; set; }

		public bool IsGroup { get; set; }

		public bool HasMixture { get; set; }

		public string RMGroupName { get; set; }

		 public int? RawMaterialGradeId { get; set; }

		 
    }
}