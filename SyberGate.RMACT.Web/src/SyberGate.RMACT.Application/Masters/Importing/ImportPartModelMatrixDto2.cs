using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Importing.Dto
{
    public class ImportPartModelMatrixDto2
    {
        public string PartNumber { get; set; }

        public int Quantity { get; set; }


        public int LeadModelId { get; set; }

        public string Exception { get; set; }

        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }


    }
}
