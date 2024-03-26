using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Models.Dtos
{
    public class MedicinalTypeDto : EntityDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public bool IsDeleted { get; set; }

    }
}