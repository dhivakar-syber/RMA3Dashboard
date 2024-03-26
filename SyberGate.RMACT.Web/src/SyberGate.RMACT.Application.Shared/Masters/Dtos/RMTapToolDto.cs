
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class RMTapToolDto : EntityDto
    {
       

           
            public virtual string RMGrade { get; set; }

            
            public virtual string RMSpec { get; set; }

         public virtual string Buyer { get; set; }

            public virtual string Supplier { get; set; }

        public virtual decimal BaseRMRate { get; set; }

        public virtual decimal RMSurchargeGradeDiff { get; set; }

        public virtual decimal SecondaryProcessing { get; set; }

        public virtual decimal SurfaceProtection { get; set; }

        public virtual decimal Thickness { get; set; }

        public virtual decimal CuttingCost { get; set; }

        public virtual decimal MOQVolume { get; set; }

        public virtual decimal Transport { get; set; }

        public virtual decimal Others { get; set; }



        public virtual DateTime CreatedOn { get; set; }

           
            public virtual DateTime Date { get; set; }

            public virtual int? SupplierId { get; set; }

        

            public virtual int? BuyerId { get; set; }

            


        }





    
}