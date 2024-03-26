﻿using System;
using Abp;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class ImportPartsFromExcelJobArgs
    {
        public int? TenantId { get; set; }

        public Guid BinaryObjectId { get; set; }

        public UserIdentifier User { get; set; }

        public virtual byte[] Bytes { get; set; }
    }
}