using System.Collections.Generic;
using Abp.Localization;
using SyberGate.RMACT.Install.Dto;

namespace SyberGate.RMACT.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
