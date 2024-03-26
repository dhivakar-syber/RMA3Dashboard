
using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Importing.Dto
{
    public class ImportApprovalUsersDto
    {

        public string UserName { get; set; }

        public string Department { get; set; }
        public string Email { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}

