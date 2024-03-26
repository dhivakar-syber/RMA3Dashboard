using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateCommodityTreeInput
    {
        public long? ParentId { get; set; }

        [Required]
        [StringLength(CommodityTreeConsts.MaxDispalayNameLength)]
        public string DisplayName { get; set; }

        public bool IsLeaf { get; set; }
    }
}