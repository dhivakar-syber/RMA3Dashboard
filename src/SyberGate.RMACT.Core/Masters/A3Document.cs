using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Masters
{
	[Table("A3Documents")]
    public class A3Document : Entity 
    {

		[Required]
		public virtual string Buyer { get; set; }
		
		[Required]
		public virtual string Supplier { get; set; }
		
		[Required]
		public virtual string Month { get; set; }
		
		[Required]
		public virtual string Year { get; set; }
		
		[Required]
		public virtual string Version { get; set; }

		public virtual bool IsApproved { get; set; }

		public virtual string Remarks { get; set; }

        public virtual bool L4Approval { get; set; }

        public virtual bool CpApproval { get; set; }

        public virtual bool FinApproval { get; set; }

        public virtual bool CommadityExpertApproval { get; set; }

        public virtual string L4Status { get; set; }


        public virtual string CpStatus { get; set; }

        public virtual string FinStatus { get; set; }

        public virtual string CommadityExpertStatus { get; set; }

        public virtual string L4remarks { get; set; }


        public virtual string Cpremarks { get; set; }


        public virtual string Finremarks { get; set; }


        public virtual string CommadityExpertremarks { get; set; }

        public virtual string L4Token { get; set; }

        public virtual string CpToken { get; set; }

        public virtual string FinToken { get; set; }

        public virtual string CommadityExpertToken { get; set; }

        public virtual string RL4Token { get; set; }


        public virtual string RCpToken { get; set; }


        public virtual string RFinToken { get; set; }


        public virtual string RCommadityExpertToken { get; set; }




    }
}