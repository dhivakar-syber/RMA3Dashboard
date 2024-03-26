using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllRMGroupsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int HasMixtureFilter { get; set; }



    }
}