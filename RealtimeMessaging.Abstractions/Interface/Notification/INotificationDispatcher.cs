using RealtimeMessaging.Abstractions.Model.Notification;

namespace RealtimeMessaging.Abstractions.Interface.Notification
{
    /// <summary>
    /// Sends real-time notifications to connected users.
    /// </summary>
    public interface INotificationDispatcher
    {
        /// <summary>
        /// Sends a notification to a specific user.
        /// </summary>
        Task SendToUserAsync(
        string userId,
        NotificationRequest notification);
        /// <summary>
        /// Sends a notification a Group.
        /// </summary>
        Task SendToGroupAsync(
            string groupName,
            NotificationRequest notification);

        /// <summary>
        /// Broadcasts a notification to all connected users.
        /// </summary>
        Task BroadcastAsync(
            NotificationRequest notification);
    }
}
