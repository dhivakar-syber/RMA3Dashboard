using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Masters
{
	[Table("ApprovalUsers")]
    public class ApprovalUser : Entity 
    {

		[Required]
		[StringLength(ApprovalUserConsts.MaxUserNameLength, MinimumLength = ApprovalUserConsts.MinUserNameLength)]
		public virtual string UserName { get; set; }
		
		[Required]
		[StringLength(ApprovalUserConsts.MaxDepartmentLength, MinimumLength = ApprovalUserConsts.MinDepartmentLength)]
		public virtual string Department { get; set; }
		
		[Required]
		[StringLength(ApprovalUserConsts.MaxEmailLength, MinimumLength = ApprovalUserConsts.MinEmailLength)]
		public virtual string Email { get; set; }
		

    }
}