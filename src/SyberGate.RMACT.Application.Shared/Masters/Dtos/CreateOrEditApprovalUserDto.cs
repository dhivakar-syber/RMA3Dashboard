
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditApprovalUserDto : EntityDto<int?>
    {

		[Required]
		[StringLength(ApprovalUserConsts.MaxUserNameLength, MinimumLength = ApprovalUserConsts.MinUserNameLength)]
		public string UserName { get; set; }
		
		
		[Required]
		[StringLength(ApprovalUserConsts.MaxDepartmentLength, MinimumLength = ApprovalUserConsts.MinDepartmentLength)]
		public string Department { get; set; }
		
		
		[Required]
		[StringLength(ApprovalUserConsts.MaxEmailLength, MinimumLength = ApprovalUserConsts.MinEmailLength)]
		public string Email { get; set; }
		
		

    }
}