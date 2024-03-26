
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditYearDto : EntityDto<int?>
    {

		[Required]
		[StringLength(YearConsts.MaxNameLength, MinimumLength = YearConsts.MinNameLength)]
		public string Name { get; set; }
		
		public int SeqNo { get; set; }

    }
}