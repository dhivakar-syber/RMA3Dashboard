using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllApprovalUsersInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string UserNameFilter { get; set; }

		public string DepartmentFilter { get; set; }

		public string EmailFilter { get; set; }



    }
}