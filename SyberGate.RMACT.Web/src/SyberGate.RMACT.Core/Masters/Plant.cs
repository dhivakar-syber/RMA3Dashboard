using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Masters
{
	[Table("Plants")]
    public class Plant : Entity 
    {

		[Required]
		[StringLength(PlantConsts.MaxCodeLength, MinimumLength = PlantConsts.MinCodeLength)]
		public virtual string Code { get; set; }
		
		[Required]
		[StringLength(PlantConsts.MaxDescriptionLength, MinimumLength = PlantConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }
		

    }
}