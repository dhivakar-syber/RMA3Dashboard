using System.Collections.Generic;
using SyberGate.RMACT.DynamicEntityProperties.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.DynamicEntityProperty
{
    public class CreateEntityDynamicPropertyViewModel
    {
        public string EntityFullName { get; set; }

        public List<string> AllEntities { get; set; }

        public List<DynamicPropertyDto> DynamicProperties { get; set; }
    }
}
