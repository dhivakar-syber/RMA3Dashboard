
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditA3DocumentDto : EntityDto<int?>
    {

		[Required]
		public string Buyer { get; set; }
		
		public int BuyerId { get; set; }
		
		[Required]
		public string Supplier { get; set; }
		
		public int SupplierId { get; set; }
		
		[Required]
		public string Month { get; set; }
		
		
		[Required]
		public string Year { get; set; }
		
		public string Period { get; set; }

		[Required]
		public int GroupId { get; set; }

		public string Group { get; set; }

        [Required]
        public int GradeId { get; set; }

        public string Grade { get; set; }

		public string Remarks { get; set; }
	}
}