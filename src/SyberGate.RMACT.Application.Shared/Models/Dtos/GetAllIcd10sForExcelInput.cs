using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Models.Dtos
{
    public class GetAllIcd10sForExcelInput
    {
        public string Filter { get; set; }

        public string CodeFilter { get; set; }

        public string NameFilter { get; set; }

        public string DiseaseChapterCodeFilter { get; set; }

        public string DiseaseChapterNameFilter { get; set; }

        public string WHOeNameFilter { get; set; }

        public string WHONameFilter { get; set; }

        public int? MaxStatusFilter { get; set; }
        public int? MinStatusFilter { get; set; }

    }
}