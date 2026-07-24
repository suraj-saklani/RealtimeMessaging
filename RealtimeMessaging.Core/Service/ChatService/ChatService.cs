using RealtimeMessaging.Abstractions.Interface.Notification;
using RealtimeMessaging.Abstractions.Model.Entities.Chat;
using RealtimeMessaging.Abstractions.Model.Notification;
using RealtimeMessaging.Core.Interface.Repositories;
using RealtimeMessaging.Core.Repositories;

namespace RealtimeMessaging.Core.Service.ChatService
{
    internal class ChatService(IChatRepository _chatRepository, IRepository<ChatMessageEntity> _chatMessageRepository, INotificationDispatcher _dispatcher) : IChatService
    {
        public async Task SendMessageAsync(string senderUserId, string receiverUserId, string message)
        {
            var chat = await _chatRepository.GetOrCreateChatAsync(senderUserId, receiverUserId);

            var chatMessageEntity = new ChatMessageEntity
            {
                SenderId = senderUserId,
                Message = message,
                ChatId = chat.Id,
            };
            await _chatMessageRepository.AddAsync(chatMessageEntity);

            chat.LastMessageDate = DateTime.UtcNow;
            chat.LastMessagePreview = ChatEntity.PreviewMessage(message);
            chat.LastMessageSenderId = senderUserId;
            await _chatRepository.UpdateAsync(chat);

            await _dispatcher.SendToUserAsync(receiverUserId, new NotificationRequest
            {
                Data = new
                {
                    ChatId = chat.Id,
                    SenderId = senderUserId,
                    Message = message,
                    Timestamp = chat.LastMessageDate
                }
            });
        }

    }
}
