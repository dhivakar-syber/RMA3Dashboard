
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditIndexNameDto : EntityDto<int?>
    {

		[Required]
		[StringLength(IndexNameConsts.MaxNameLength, MinimumLength = IndexNameConsts.MinNameLength)]
		public string Name { get; set; }
		
		

    }
}