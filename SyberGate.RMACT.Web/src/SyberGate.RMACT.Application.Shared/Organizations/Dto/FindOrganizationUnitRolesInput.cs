using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Organizations.Dto
{
    public class FindOrganizationUnitRolesInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}