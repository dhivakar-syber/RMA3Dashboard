using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class SupportAttachmentsDto : EntityDto 
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
