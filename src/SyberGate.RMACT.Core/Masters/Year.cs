using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Masters
{
	[Table("Years")]
    public class Year : Entity 
    {

		[Required]
		[StringLength(YearConsts.MaxNameLength, MinimumLength = YearConsts.MinNameLength)]
		public virtual string Name { get; set; }

		public virtual int SeqNo { get; set; }
    }
}