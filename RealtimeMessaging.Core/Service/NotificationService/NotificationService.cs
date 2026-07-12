using RealtimeMessaging.Abstractions.Entities.Notifiacton;
using RealtimeMessaging.Abstractions.Interface.Notification;
using RealtimeMessaging.Abstractions.Model.Notification;
using RealtimeMessaging.Core.Repositories;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace RealtimeMessaging.Core.Service.NotificationService
{
    public class NotificationService(INotificationDispatcher _dispatcher, IRepository<NotificationEntity> _notificationRepository = null) : INotificationService
    {
        public async Task NotifyUserAsync(string userId, NotificationRequest request)
        {

            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException(nameof(userId));

            await AddNotification(new NotificationEntity
            {
                UserId = userId,
                Title = request.Title,
                Message = request.Message
            });

            await _dispatcher.SendToUserAsync(userId, request);
        }

        public async Task<IList<NotificationEntity>> GetAllNotification(Expression<Func<NotificationEntity, bool>> exp)
        {
            return await _notificationRepository.GetAll(exp);
        }

        public async Task<NotificationEntity> AddNotification(NotificationEntity notificationEntity)
        {
            return await _notificationRepository.AddAsync(notificationEntity);
        }
        
        public async Task<NotificationEntity> MarkAsRead(Guid notificatoinId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificatoinId);
            if (notification == null)
                throw new ArgumentException(nameof(notificatoinId));
            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
            return await _notificationRepository.UpdateAsync(notification);
        }

    }
}
