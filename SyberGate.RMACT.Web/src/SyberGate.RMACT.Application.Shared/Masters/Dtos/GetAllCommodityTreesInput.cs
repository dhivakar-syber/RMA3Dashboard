using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllCommodityTreesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string DispalayNameFilter { get; set; }



    }
}