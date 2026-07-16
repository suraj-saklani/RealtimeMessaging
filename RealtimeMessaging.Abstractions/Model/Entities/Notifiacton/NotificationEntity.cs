namespace RealtimeMessaging.Abstractions.Entities.Notifiacton
{
    /// <summary>
    /// Represents a persisted notification stored in the database.
    /// </summary>
    public class NotificationEntity : BaseEntity
    {
        /// <summary>
        /// User who owns the notification.
        /// </summary>
        public string NotifiedTo { get; set; }

        /// <summary>
        /// Notification type indicating whether it is for a user, group, or broadcast.
        /// </summary>
        public NotifiedToEnum NotifiedToType { get; set; }

        /// <summary>
        /// Notification title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Main notification message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Notification category.
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Optional custom data.
        /// </summary>
        public string? Data { get; set; }
        /// <summary>
        /// Indicates whether the notification has been read.
        /// </summary>
        public bool IsRead { get; set; }
        /// <summary>
        /// Date and time when the notification was marked as read.
        /// </summary>
        public DateTime? ReadAt { get; set; }

    }
    public enum NotifiedToEnum
    {
        User,
        Group,
        Broadcast
    }
}
