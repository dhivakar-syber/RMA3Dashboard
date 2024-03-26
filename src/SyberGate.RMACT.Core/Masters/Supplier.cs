using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("Suppliers")]
    [Audited]
    public class Supplier : Entity 
    {

		[Required]
		[StringLength(SupplierConsts.MaxCodeLength, MinimumLength = SupplierConsts.MinCodeLength)]
		public virtual string Code { get; set; }
		
		[Required]
		[StringLength(SupplierConsts.MaxNameLength, MinimumLength = SupplierConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}