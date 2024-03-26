using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetRawMaterialGradeForEditOutput
    {
		public CreateOrEditRawMaterialGradeDto RawMaterialGrade { get; set; }

		public string RawMaterialGradeName { get; set;}


    }
}