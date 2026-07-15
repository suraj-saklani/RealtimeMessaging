using Microsoft.AspNetCore.SignalR;

namespace RealtimeMessaging.SignalR.Hubs;

public class NotificationHub: Hub
{
    public async override Task OnConnectedAsync()
    {
        Console.WriteLine(Context.ConnectionId);
        Console.WriteLine(Context.UserIdentifier);
        await base.OnConnectedAsync();
    }
}

