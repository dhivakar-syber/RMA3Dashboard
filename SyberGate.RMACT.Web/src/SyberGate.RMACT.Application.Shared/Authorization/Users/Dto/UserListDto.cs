using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SyberGate.RMACT.Authorization.Users.Dto
{
    public class UserListDto : EntityDto<long>, IPassivable, IHasCreationTime
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public List<UserListRoleDto> Roles { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }

        public string L4EmailAddress { get; set; }
        public string CpEmailAddress { get; set; }
        public string FinEmailAddress { get; set; }

        public string CommadityExpertEmailAddress { get; set; }

        public string L4UserName { get; set; }

        public string L4Department { get; set; }

        public string CpUserName { get; set; }

        public string CpDepartment { get; set; }

        public string FinUserName { get; set; }

        public string FinDepartment { get; set; }

        public string CommadityExpertUserName { get; set; }

        public string CommadityExpertDepartment { get; set; }

    }
}