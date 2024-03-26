using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetPartModelMatrixForEditOutput
    {
		public CreateOrEditPartModelMatrixDto PartModelMatrix { get; set; }

		public string LeadModelName { get; set;}


    }
}