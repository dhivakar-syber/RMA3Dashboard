using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dtos
{
	public class LeadModelGraphDto
	{
		public int LeadModelId { get; set; }

		public string LeadModelName { get; set; }

		public decimal CurrentRM { get; set; }

		public decimal RevisedRM { get; set; }
	}
}
