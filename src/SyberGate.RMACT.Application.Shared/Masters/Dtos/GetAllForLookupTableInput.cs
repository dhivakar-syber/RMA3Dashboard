using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}