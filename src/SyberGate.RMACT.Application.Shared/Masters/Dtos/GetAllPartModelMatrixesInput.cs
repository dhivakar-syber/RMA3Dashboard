using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllPartModelMatrixesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string PartNumberFilter { get; set; }


		 public string LeadModelNameFilter { get; set; }

		 
    }
}