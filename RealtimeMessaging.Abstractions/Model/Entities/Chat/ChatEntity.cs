using RealtimeMessaging.Abstractions.Entities;

namespace RealtimeMessaging.Abstractions.Model.Entities.Chat
{
    public class ChatEntity : BaseEntity
    {
        public Guid LastMessageId { get; set; }
        public DateTime LastMessageDate { get; set; }
        public string? LastMessagePreview { get; set; }
        public string? LastMessageSenderId { get; set; }
        public string ChatKey { get; set; }

        public static string GetChatKey(string userId1, string userId2)
        {
            return string.CompareOrdinal(userId1, userId2) < 0
                ? $"{userId1}:{userId2}"
                : $"{userId2}:{userId1}";
        }
        public static string PreviewMessage(string message, int maxLength = 100)
        {
            if (string.IsNullOrEmpty(message))
                return string.Empty;
            return message.Length <= maxLength ? message : message.Substring(0, maxLength);
        }

    }
}
