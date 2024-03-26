using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace SyberGate.RMACT.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public string Supplier { get; set; }
        public string Buyer { get; set; }
        public string Period { get; set; }
        public string Partno { get; set; }
        public bool IsGenerateA3 { get; set; }
        public int A3Id { get; set; }
        public string SupplierName { get; set; }
        public string BuyerName { get; set; }
        public string Plant { get; set; }
        public string TemplatePath { get; set; }
        public string Group { get; set; }

        public string apppath { get; set; }
    }
}