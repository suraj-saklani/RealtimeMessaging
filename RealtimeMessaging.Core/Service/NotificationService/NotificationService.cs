using RealtimeMessaging.Abstractions.Entities.Notifiacton;
using RealtimeMessaging.Abstractions.Interface.Notification;
using RealtimeMessaging.Abstractions.Model.Notification;
using RealtimeMessaging.Core.Repositories;
using System.Linq.Expressions;

namespace RealtimeMessaging.Core.Service.NotificationService
{
    internal class NotificationService(INotificationDispatcher _dispatcher, IRepository<NotificationEntity> _notificationRepository = null) : INotificationService
    {
        public async Task NotifyUserAsync(string userId, NotificationRequest request, bool saveInDB = false)
        {

            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException(nameof(userId));

            if (saveInDB)
            {
                await AddNotification(new NotificationEntity
                {
                    NotifiedTo = userId,
                    Title = request.Title,
                    Message = request.Message,
                    IsRead = false,
                    Type = request.Type,
                    Data = request.Data,
                    NotifiedToType = NotifiedToEnum.User
                });
            }

            await _dispatcher.SendToUserAsync(userId, request);
        }

        public async Task NotifyBroadcastAsync(NotificationRequest request, bool saveInDB = false)
        {
            if (saveInDB)
            {
                await AddNotification(new NotificationEntity
                {
                    Title = request.Title,
                    Message = request.Message,
                    IsRead = false,
                    Type = request.Type,
                    Data = request.Data,
                    NotifiedToType = NotifiedToEnum.Broadcast,
                    NotifiedTo = "Broadcast"
                });
            }

            await _dispatcher.BroadcastAsync(request);
        }

        public async Task NotifyGroupAsync(string groupId, NotificationRequest request, bool saveInDB = false)
        {
            if (saveInDB)
            {
                await AddNotification(new NotificationEntity
                {
                    Title = request.Title,
                    Message = request.Message,
                    IsRead = false,
                    Type = request.Type,
                    Data = request.Data,
                    NotifiedToType = NotifiedToEnum.Group,
                    NotifiedTo = groupId
                });
            }

            await _dispatcher.SendToGroupAsync(groupId, request);
        }

        public async Task<IList<NotificationEntity>> GetAllNotification(Expression<Func<NotificationEntity, bool>> exp)
        {
            return await _notificationRepository.GetAll(exp);
        }

        public async Task<NotificationEntity> AddNotification(NotificationEntity notificationEntity)
        {
            return await _notificationRepository.AddAsync(notificationEntity);
        }
        
        public async Task<NotificationEntity> MarkAsRead(Guid notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification == null)
                throw new ArgumentException(nameof(notificationId));
            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
            return await _notificationRepository.UpdateAsync(notification);
        }

        public async Task MarkNotificationAsRead(Guid notificationId)
        {
            await MarkAsRead(notificationId);
        }
    }
}
