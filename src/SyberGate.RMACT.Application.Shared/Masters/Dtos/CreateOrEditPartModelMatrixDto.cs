
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditPartModelMatrixDto : EntityDto<int?>
    {

		[Required]
		[StringLength(PartModelMatrixConsts.MaxPartNumberLength, MinimumLength = PartModelMatrixConsts.MinPartNumberLength)]
		public string PartNumber { get; set; }
		
		
		[Range(PartModelMatrixConsts.MinQuantityValue, PartModelMatrixConsts.MaxQuantityValue)]
		public int Quantity { get; set; }
		
		
		 public int LeadModelId { get; set; }
		 
		 
    }
}