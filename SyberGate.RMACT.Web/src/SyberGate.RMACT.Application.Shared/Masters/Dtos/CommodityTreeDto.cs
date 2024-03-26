using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CommodityTreeDto : AuditedEntityDto<long>
    {
        public long? ParentId { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }

        public int MemberCount { get; set; }

        public int RoleCount { get; set; }

        public bool IsLeaf { get; set; }
    }
}