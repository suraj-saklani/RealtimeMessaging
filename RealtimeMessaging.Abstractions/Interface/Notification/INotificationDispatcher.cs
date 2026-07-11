using RealtimeMessaging.Abstractions.Model.Notification;

namespace RealtimeMessaging.Abstractions.Interface.Notification
{
    public interface INotificationDispatcher
    {
        Task SendToUserAsync(
        string userId,
        NotificationRequest notification);

        Task SendToGroupAsync(
            string groupName,
            NotificationRequest notification);

        Task BroadcastAsync(
            NotificationRequest notification);
    }
}
