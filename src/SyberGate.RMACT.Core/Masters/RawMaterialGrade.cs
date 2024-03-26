using SyberGate.RMACT.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("RawMaterialGrades")]
    [Audited]
    public class RawMaterialGrade : Entity 
    {

		[Required]
		[StringLength(RawMaterialGradeConsts.MaxNameLength, MinimumLength = RawMaterialGradeConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		public virtual bool IsGroup { get; set; }
		
		public virtual bool HasMixture { get; set; }
		
		public virtual int? RawMaterialGradeId { get; set; }
		
        [ForeignKey("RawMaterialGradeId")]
		public RawMaterialGrade RawMaterialGradeFk { get; set; }

		
	}
}