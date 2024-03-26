using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("A3LeadModels")]
    [Audited]
    public class A3LeadModel : Entity 
    {
		public virtual int DocId { get; set; }
		[Required]
		[StringLength(LeadModelConsts.MaxNameLength, MinimumLength = LeadModelConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		[Required]
		[StringLength(LeadModelConsts.MaxDescriptionLength, MinimumLength = LeadModelConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }

        [ForeignKey("LeadModelId")]
        public LeadModel LeadModelFk { get; set; }


    }
}