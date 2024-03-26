using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Organizations.Dto
{
    public class FindOrganizationUnitUsersInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
