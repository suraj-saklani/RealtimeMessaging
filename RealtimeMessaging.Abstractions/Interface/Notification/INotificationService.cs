using RealtimeMessaging.Abstractions.Model.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeMessaging.Abstractions.Interface.Notification
{
    public interface INotificationService
    {
        Task NotifyBroadcastAsync(NotificationRequest request, bool saveInDB = false);
        Task NotifyGroupAsync(string groupId, NotificationRequest request, bool saveInDB = false);
        Task NotifyUserAsync(string userId, NotificationRequest request, bool saveInDB = false);
    }
}
