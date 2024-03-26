using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("UnitOfMeasurements")]
    [Audited]
    public class UnitOfMeasurement : Entity 
    {

		[Required]
		[StringLength(UnitOfMeasurementConsts.MaxCodeLength, MinimumLength = UnitOfMeasurementConsts.MinCodeLength)]
		public virtual string Code { get; set; }
		
		[Required]
		[StringLength(UnitOfMeasurementConsts.MaxNameLength, MinimumLength = UnitOfMeasurementConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}