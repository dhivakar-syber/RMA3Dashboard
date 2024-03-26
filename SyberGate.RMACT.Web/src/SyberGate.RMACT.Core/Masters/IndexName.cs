using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("IndexNames")]
    [Audited]
    public class IndexName : Entity 
    {

		[Required]
		[StringLength(IndexNameConsts.MaxNameLength, MinimumLength = IndexNameConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}