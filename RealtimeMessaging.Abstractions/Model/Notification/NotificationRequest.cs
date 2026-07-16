namespace RealtimeMessaging.Abstractions.Model.Notification;

public class NotificationRequest
{
    public string Title { get; set; }

    public string Message { get; set; }
    public string? Type { get; set; }
    public string? Data { get; set; }

}

