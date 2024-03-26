using Abp;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dtos
{
    
    public class ImportApprovalUsersFromJobArgs
    {

        public int? TenantId { get; set; }

        public Guid BinaryObjectId { get; set; }

        public UserIdentifier User { get; set; }

        public virtual byte[] Bytes { get; set; }
    }
}
