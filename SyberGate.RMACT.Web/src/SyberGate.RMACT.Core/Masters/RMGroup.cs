using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("RMGroups")]
    [Audited]
    public class RMGroup : Entity 
    {

		[Required]
		[StringLength(RMGroupConsts.MaxNameLength, MinimumLength = RMGroupConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		public virtual bool HasMixture { get; set; }
		

    }
}