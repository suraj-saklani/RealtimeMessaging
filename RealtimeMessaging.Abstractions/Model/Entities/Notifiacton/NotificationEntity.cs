namespace RealtimeMessaging.Abstractions.Entities.Notifiacton
{
    public class NotificationEntity : BaseEntity
    {
        public string UserId { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string? Type { get; set; }

        public string? Data { get; set; }

        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
