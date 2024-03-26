using SyberGate.RMACT.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("A3PartModelMatrixes")]
    [Audited]
    public class A3PartModelMatrix : Entity 
    {
		public virtual int DocId { get; set; }

		[Required]
		[StringLength(PartModelMatrixConsts.MaxPartNumberLength, MinimumLength = PartModelMatrixConsts.MinPartNumberLength)]
		public virtual string PartNumber { get; set; }

		public virtual string Description { get; set; }

		[Range(PartModelMatrixConsts.MinQuantityValue, PartModelMatrixConsts.MaxQuantityValue)]
		public virtual int Quantity { get; set; }
		

		public virtual int LeadModelId { get; set; }
		
        [ForeignKey("LeadModelId")]
		public LeadModel LeadModelFk { get; set; }
		
    }
}