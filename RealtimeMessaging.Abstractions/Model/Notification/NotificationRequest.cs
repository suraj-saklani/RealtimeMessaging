namespace RealtimeMessaging.Abstractions.Model.Notification;

public class NotificationRequest
{
    public string Title { get; set; }

    public string Message { get; set; }

    public object? Data { get; set; }
}

