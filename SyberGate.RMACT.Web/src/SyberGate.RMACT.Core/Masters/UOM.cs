using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("UOMs")]
    [Audited]
    public class UOM : Entity 
    {

		[Required]
		[StringLength(UOMConsts.MaxCodeLength, MinimumLength = UOMConsts.MinCodeLength)]
		public virtual string Code { get; set; }
		
		[Required]
		[StringLength(UOMConsts.MaxDescriptionLength, MinimumLength = UOMConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }
		

    }
}