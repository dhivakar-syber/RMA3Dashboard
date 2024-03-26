
using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllSupportAttchmentsInput : PagedAndSortedResultRequestDto
    {
        public int A3IdFilter { get; set; }

        public string BuyerFilter { get; set; }

        public string SupplierFilter { get; set; }


        public string VersionFilter { get; set; }

        


    }
}