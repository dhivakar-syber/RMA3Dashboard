using System;
using Abp.Notifications;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}