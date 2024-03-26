using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace SyberGate.RMACT.Masters.Importing.Dto
{
    [AutoMap(typeof(Part), typeof(SubPart))]
    public class ImportPartDto
    {
        public string Supplier { get; set; }

        public string Buyer { get; set; }

        public string PartNo { get; set; }

        public string Description { get; set; }

        public string SubPartNo { get; set; }

        public string SubPartDescription { get; set; }

        public bool IsParent { get; set; }

        public string RMGroup { get; set; }

        public string RMGrade { get; set; }

        public decimal? GrossInputWeight { get; set; }

        public decimal? CastingForgingWeight { get; set; }

        public decimal? FinishedWeight { get; set; }

        public decimal? ScrapRecoveryPercent { get; set; }

        public decimal? RMReferenceCost { get; set; }

        public string RMReference { get; set; }

        //public decimal? LogisticsCost { get; set; }

        public string Exception { get; set; }

        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
