
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class PartBucketDto : EntityDto
    {
		public string RMSpec { get; set; }

		public string Buckets { get; set; }

		public decimal Value { get; set; }

		public string Buyer { get; set; }

		public string Supplier { get; set; }

		public string Month { get; set; }


		public string Year { get; set; }





    }
}