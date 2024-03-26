using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("RawMaterialIndexes")]
    [Audited]
    public class RawMaterialIndex : Entity 
    {

		public virtual Months Month { get; set; }
		
		public virtual decimal? Value { get; set; }
		

		public virtual int IndexNameId { get; set; }
		
        [ForeignKey("IndexNameId")]
		public IndexName IndexNameFk { get; set; }
		
		public virtual int YearId { get; set; }
		
        [ForeignKey("YearId")]
		public Year YearFk { get; set; }
		
		public virtual int RawMaterialGradeId { get; set; }
		
        [ForeignKey("RawMaterialGradeId")]
		public RawMaterialGrade RawMaterialGradeFk { get; set; }
		
    }
}