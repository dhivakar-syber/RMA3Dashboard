using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Models.Dtos
{
    public class GetAllMedicalSpecialtiesForExcelInput
    {
        public string Filter { get; set; }

        public string CodeFilter { get; set; }

        public string NameFilter { get; set; }

        public string FullnameFilter { get; set; }

        public string DecisionCodeFilter { get; set; }

        public int? MaxStatusFilter { get; set; }
        public int? MinStatusFilter { get; set; }

    }
}