using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Models.Dtos
{
    public class GetGenderForEditOutput
    {
        public CreateOrEditGenderDto Gender { get; set; }

    }
}