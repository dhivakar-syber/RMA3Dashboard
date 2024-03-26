using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Importing.Dto
{
	public class ImportPartBucketDto
	{
        public string RMGrade { get; set; }
        public string RMSpec { get; set; }

		public string Buckets { get; set; }

		public decimal Value { get; set; }

		public string  Buyer { get; set; }

		public string Supplier { get; set; }

        public virtual decimal BaseRMRate { get; set; }

        public virtual decimal RMSurchargeGradeDiff { get; set; }

        public virtual decimal SecondaryProcessing { get; set; }

        public virtual decimal SurfaceProtection { get; set; }

        public virtual decimal Thickness { get; set; }

        public virtual decimal CuttingCost { get; set; }

        public virtual decimal MOQVolume { get; set; }

        public virtual decimal Transport { get; set; }

        public virtual decimal Others { get; set; }



        public DateTime CreatedOn { get; set; }

        public virtual DateTime Date { get; set; }

        


        public string Month { get; set; }

		public string Year { get; set; }
			 
		public string Exception { get; set; }

		public bool CanBeImported()
		{
			return string.IsNullOrEmpty(Exception);
		}


	}
}


