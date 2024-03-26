using Abp.Extensions;
using SyberGate.RMACT.Authorization.Users;
using SyberGate.RMACT.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Web.Models.Account
{
    public class EmailViewModel
    {
        public string Buyername { get; set; }

        public string Suppliername { get; set; }

        public int A3Id { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }

        public bool Sequence { get; set; }

        public int uid { get; set; }

        public string BuyerRemarks { get; set; }

        public string Version { get; set; }

        public bool IsExternalLogin { get; set; }

        public string ReturnUrl { get; set; }

        public string SingleSignIn { get; set; }

        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

       
    }
}
