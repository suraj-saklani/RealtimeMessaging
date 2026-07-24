using RealtimeMessaging.Abstractions.Model.Entities.Chat;
using RealtimeMessaging.Core.Repositories;

namespace RealtimeMessaging.Core.Interface.Repositories
{
    public interface IChatRepository : IRepository<ChatEntity>
    {
        Task<ChatEntity> GetOrCreateChatAsync(string senderUserId, string receiverUserId);
    }
}
