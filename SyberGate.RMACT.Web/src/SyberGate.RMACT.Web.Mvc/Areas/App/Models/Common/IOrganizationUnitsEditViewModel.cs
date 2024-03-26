using System.Collections.Generic;
using SyberGate.RMACT.Organizations.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.Common
{
    public interface IOrganizationUnitsEditViewModel
    {
        List<OrganizationUnitDto> AllOrganizationUnits { get; set; }

        List<string> MemberedOrganizationUnits { get; set; }
    }
}