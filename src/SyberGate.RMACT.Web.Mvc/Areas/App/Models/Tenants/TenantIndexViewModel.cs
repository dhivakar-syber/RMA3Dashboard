using System.Collections.Generic;
using SyberGate.RMACT.Editions.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.Tenants
{
    public class TenantIndexViewModel
    {
        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }
    }
}