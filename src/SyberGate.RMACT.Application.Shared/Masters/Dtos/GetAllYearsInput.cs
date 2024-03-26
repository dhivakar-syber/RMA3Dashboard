using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllYearsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}