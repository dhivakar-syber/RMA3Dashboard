using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Importing.Dto
{
    public class ImportLeadModelsDto
    {

        public string Name { get; set; }

        public string Description { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
