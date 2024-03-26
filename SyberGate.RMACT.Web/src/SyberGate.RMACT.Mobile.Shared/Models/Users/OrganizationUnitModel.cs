using Abp.AutoMapper;
using SyberGate.RMACT.Organizations.Dto;

namespace SyberGate.RMACT.Models.Users
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}