
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditLeadModelDto : EntityDto<int?>
    {

		[Required]
		[StringLength(LeadModelConsts.MaxNameLength, MinimumLength = LeadModelConsts.MinNameLength)]
		public string Name { get; set; }
		
		
		[Required]
		[StringLength(LeadModelConsts.MaxDescriptionLength, MinimumLength = LeadModelConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		

    }
}