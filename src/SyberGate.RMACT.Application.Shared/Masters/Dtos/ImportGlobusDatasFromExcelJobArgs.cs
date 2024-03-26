using System;
using Abp;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class ImportGlobusDatasFromExcelJobArgs
    {
        public int? TenantId { get; set; }

        public Guid BinaryObjectId { get; set; }

        public UserIdentifier User { get; set; }

        public virtual byte[] Bytes { get; set; }
    }
}