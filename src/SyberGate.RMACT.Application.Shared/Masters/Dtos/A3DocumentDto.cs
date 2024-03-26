
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
	public class A3DocumentDto : EntityDto
	{
		public string Buyer { get; set; }

		public string Supplier { get; set; }

		public string Month { get; set; }

		public string Year { get; set; }

		public string Version { get; set; }
		
		public bool IsApproved { get; set; }

		public string Remarks { get; set; }

        public virtual bool L4Approval { get; set; }

        public virtual bool CpApproval { get; set; }

        public virtual bool FinApproval { get; set; }

        public virtual bool CommadityExpertApproval { get; set; }

        public virtual string L4Status { get; set; }


        public virtual string CpStatus { get; set; }

        public virtual string FinStatus { get; set; }

        public virtual string CommadityStatus { get; set; }

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