using RealtimeMessaging.Abstractions.Entities.Notifiacton;
using RealtimeMessaging.Abstractions.Model.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeMessaging.Abstractions.Interface.Notification
{
    public interface INotificationService
    {
        Task<IList<NotificationEntity>> GetAllNotificationAsync(Expression<Func<NotificationEntity, bool>>? exp = null);
        Task<NotificationEntity> MarkNotificationAsReadAsync(Guid notificationId);
        Task NotifyBroadcastAsync(NotificationRequest request, bool saveInDB = false);
        Task NotifyGroupAsync(string groupId, NotificationRequest request, bool saveInDB = false);
        Task NotifyUserAsync(string userId, NotificationRequest request, bool saveInDB = false);
    }
}
