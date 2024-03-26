using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Authorization.Users.Dto
{
    //Mapped to/from User in CustomDtoMapper
    public class UserEditDto : IPassivable
    {
        /// <summary>
        /// Set null to create a new user. Set user's Id to update a user
        /// </summary>
        public long? Id { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string L4EmailAddress { get; set; }
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string CpEmailAddress { get; set; }
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string FinEmailAddress { get; set; }
        public string L4UserName { get; set; }

        public string L4Department { get; set; }

        public string CpUserName { get; set; }

        public string CpDepartment { get; set; }

        public string FinUserName { get; set; }

        public string FinDepartment { get; set; }

        public string CommadityExpertUserName { get; set; }

        public string CommadityExpertDepartment { get; set; }



        public string CommadityExpertEmailAddress { get; set; }

        public bool SequenceCheckBox { get; set; }


        [StringLength(UserConsts.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        // Not used "Required" attribute since empty value is used to 'not change password'
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool ShouldChangePasswordOnNextLogin { get; set; }

        public virtual bool IsTwoFactorEnabled { get; set; }

        public virtual bool IsLockoutEnabled { get; set; }

    }
}