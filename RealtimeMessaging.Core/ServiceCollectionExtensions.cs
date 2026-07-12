using RealtimeMessaging.Core.Service.NotificationService;
using RealtimeMessaging.Abstractions.Interface.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace RealtimeMessaging.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRealtimeNotificationsCore(
        this IServiceCollection services)
        {
            services.AddScoped<
                INotificationService,
                NotificationService>();
         
            return services;
        }
    }
}
