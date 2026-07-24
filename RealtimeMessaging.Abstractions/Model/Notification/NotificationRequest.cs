namespace RealtimeMessaging.Abstractions.Model.Notification;
/// <summary>
/// Represents a notification sent to one or more users.
/// </summary>
public class NotificationRequest
{
    /// <summary>
    /// Notification title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Main notification message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Notification type such as Success, Warning or Error.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Additional payload serialized as JSON.
    /// </summary>
    public Object? Data { get; set; }

    /// <summary>
    /// Client-side SignalR method to invoke.
    /// Defaults to <c>ReceiveMessage</c>.
    /// </summary>
    public string MethodName { get; set; } = "ReceiveMessage";
}

