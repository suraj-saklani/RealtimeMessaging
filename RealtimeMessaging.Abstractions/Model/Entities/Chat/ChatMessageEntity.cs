using RealtimeMessaging.Abstractions.Entities;

namespace RealtimeMessaging.Abstractions.Model.Entities.Chat
{
    public class ChatMessageEntity : BaseEntity
    {
        public string Message { get; set; }
        public string SenderId { get; set; }
        public Guid ChatId { get; set; }

    }
}
