using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class UpdateCommodityTreeInput
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [Required]
        [StringLength(CommodityTreeConsts.MaxDispalayNameLength)]
        public string DisplayName { get; set; }

        public bool IsLeaf { get; set; }
    }
}