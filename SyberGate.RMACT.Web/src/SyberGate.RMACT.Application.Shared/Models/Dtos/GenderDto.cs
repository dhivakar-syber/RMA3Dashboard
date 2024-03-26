using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Models.Dtos
{
    public class GenderDto : EntityDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

    }
}