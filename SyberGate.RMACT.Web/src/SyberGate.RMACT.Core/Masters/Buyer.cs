using SyberGate.RMACT.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("Buyers")]
    [Audited]
    public class Buyer : Entity 
    {

		[Required]
		[StringLength(BuyerConsts.MaxCodeLength, MinimumLength = BuyerConsts.MinCodeLength)]
		public virtual string Code { get; set; }
		
		[Required]
		[StringLength(BuyerConsts.MaxNameLength, MinimumLength = BuyerConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

		public virtual long? UserId { get; set; }
		
        [ForeignKey("UserId")]
		public User UserFk { get; set; }
        public virtual string Department { get; set; }

    }
}