using Microsoft.Extensions.DependencyInjection;
using RealtimeMessaging.Abstractions.Interface.Notification;
using RealtimeMessaging.Core.Service.ChatService;
using RealtimeMessaging.Core.Service.NotificationService;

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

            services.AddScoped<
                IChatService,
                ChatService>();

            return services;
        }
    }
}
