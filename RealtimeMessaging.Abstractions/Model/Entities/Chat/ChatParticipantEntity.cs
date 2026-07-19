using RealtimeMessaging.Abstractions.Entities;

namespace RealtimeMessaging.Abstractions.Model.Entities.Chat
{
    public class ChatParticipantEntity : BaseEntity
    {
        public Guid ChatId { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; } // e.g., "admin", "member", etc.
        public DateTime JoinedAt { get; set; }
    }
}
