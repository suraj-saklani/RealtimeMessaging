using RealtimeMessaging.Abstractions.Interface.Notification;
using RealtimeMessaging.Abstractions.Model.Notification;

namespace RealtimeMessaging.Core.Service.NotificationService
{
    public class NotificationService(INotificationDispatcher _dispatcher) : INotificationService
    {
        public async Task NotifyUserAsync(string userId, NotificationRequest request)
        {

            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException(nameof(userId));

            await _dispatcher.SendToUserAsync(userId, request);
        }
    }
}
