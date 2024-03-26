using System.Collections.Generic;
using SyberGate.RMACT.DynamicEntityProperties.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.DynamicProperty
{
    public class CreateOrEditDynamicPropertyViewModel
    {
        public DynamicPropertyDto DynamicPropertyDto { get; set; }

        public List<string> AllowedInputTypes { get; set; }
    }
}
