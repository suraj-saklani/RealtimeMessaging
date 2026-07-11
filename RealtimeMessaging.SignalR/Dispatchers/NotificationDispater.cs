using Microsoft.AspNetCore.SignalR;
using RealtimeMessaging.Abstractions.Interface.Notification;
using RealtimeMessaging.Abstractions.Model.Notification;
using RealtimeMessaging.SignalR.Hubs;

namespace RealtimeMessaging.SignalR.Dispatchers
{
    public class NotificationDispater(NotificationHub _hub) : INotificationDispatcher
    {
        public async Task BroadcastAsync(NotificationRequest notification)
        {
            await _hub.Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public async Task SendToGroupAsync(string groupName, NotificationRequest notification)
        {
            await _hub.Clients.Groups(groupName).SendAsync(groupName, notification);
        }

        public async Task SendToUserAsync(string userId, NotificationRequest notification)
        {
            await _hub.Clients.Client(userId).SendAsync(userId, notification);
        }
    }
}
