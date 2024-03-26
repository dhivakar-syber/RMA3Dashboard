using System;
using Abp.AutoMapper;
using SyberGate.RMACT.Sessions.Dto;

namespace SyberGate.RMACT.Models.Common
{
    [AutoMapFrom(typeof(ApplicationInfoDto)),
     AutoMapTo(typeof(ApplicationInfoDto))]
    public class ApplicationInfoPersistanceModel
    {
        public string Version { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}