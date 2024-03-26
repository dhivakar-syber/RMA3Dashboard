
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class ApprovalUserDto : EntityDto
    {
		public string UserName { get; set; }

		public string Department { get; set; }

		public string Email { get; set; }



    }
}