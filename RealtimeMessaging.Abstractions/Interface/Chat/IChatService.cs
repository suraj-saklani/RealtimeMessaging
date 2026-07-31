using RealtimeMessaging.Abstractions.Model.Entities.Chat;

public interface IChatService
{
    Task<IList<ChatEntity>> GetChatByUser(string userId);
    Task<IList<ChatMessageEntity>> GetChatMessages(Guid chatId);
    Task SendMessageAsync(string senderUserId, string receiverUserId, string message);
}