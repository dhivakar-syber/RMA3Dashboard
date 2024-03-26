using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Models.Dtos
{
    public class GetIcd10ForEditOutput
    {
        public CreateOrEditIcd10Dto Icd10 { get; set; }

    }
}