using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Models.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}