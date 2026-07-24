using Microsoft.EntityFrameworkCore;
using RealtimeMessaging.Abstractions.Model.Entities.Chat;
using RealtimeMessaging.Core.Interface.Repositories;
using RealtimeMessaging.Persistence.DbContexts;

namespace RealtimeMessaging.Persistence.Repositories
{
    internal class ChatRepository : Repository<ChatEntity>, IChatRepository
    {
        private readonly RealtimeNotificationsDbContext dbContext;

        public ChatRepository(RealtimeNotificationsDbContext dbContext): base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ChatEntity> GetOrCreateChatAsync(string senderUserId, string receiverUserId)
        {
            var chatQuery = dbContext.Chats
                .Where(c => c.ChatKey == ChatEntity.GetChatKey(senderUserId, receiverUserId))
                .AsNoTracking();

            var chat = chatQuery.FirstOrDefault();
            if (chat != null)
                return chat;

            var chatEntity = new ChatEntity
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ChatKey = string.CompareOrdinal(senderUserId, receiverUserId) < 0
                    ? $"{senderUserId}:{receiverUserId}"
                    : $"{receiverUserId}:{senderUserId}"
            };

            await dbContext.AddAsync(chatEntity);

            var chatParticipants = new List<ChatParticipantEntity>
            {
                new ChatParticipantEntity
                {
                    ChatId = chatEntity.Id,
                    UserId = senderUserId,
                    CreatedAt = DateTime.UtcNow
                },
                new ChatParticipantEntity
                {
                    ChatId = chatEntity.Id,
                    UserId = receiverUserId,
                    CreatedAt = DateTime.UtcNow
                }
            };

            foreach (var participant in chatParticipants)
            {
                await dbContext.AddAsync(participant);
            }
            return chatEntity;
        }
    }
}
