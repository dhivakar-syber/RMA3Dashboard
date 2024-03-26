using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Models.Dtos
{
    public class GetAllMedicinalTypesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

    }
}