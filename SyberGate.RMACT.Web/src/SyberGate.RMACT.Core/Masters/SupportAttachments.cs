using SyberGate.RMACT.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
    [Table("SupportAttachments")]
    [Audited]
    public class SupportAttachments : Entity
    {
        public virtual int A3Id { get; set; }


        public virtual string SupportAttachmentPath { get; set; }

        public virtual string Version { get; set; }

        public virtual string Buyer { get; set; }

        public virtual string Supplier { get; set; }

        public virtual string FileName { get; set; }

        public virtual byte[] Filebyte { get; set; }




    }
}