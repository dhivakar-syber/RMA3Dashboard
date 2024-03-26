using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetRawMaterialIndexForEditOutput
    {
		public CreateOrEditRawMaterialIndexDto RawMaterialIndex { get; set; }

		public string IndexNameName { get; set;}

		public string YearName { get; set;}

		public string RawMaterialGradeName { get; set;}


    }
}