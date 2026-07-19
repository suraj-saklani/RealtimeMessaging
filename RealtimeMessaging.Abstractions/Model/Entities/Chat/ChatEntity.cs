using RealtimeMessaging.Abstractions.Entities;

namespace RealtimeMessaging.Abstractions.Model.Entities.Chat
{
    public class ChatEntity : BaseEntity
    {
        public Guid LastMessageId { get; set; }
        public DateTime LastMessageDate { get; set; }
        public string LastMessagePreview { get; set; }
        public string LastMessageSenderId { get; set; }

    }
}
