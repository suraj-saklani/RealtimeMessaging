using Microsoft.Extensions.DependencyInjection;
using RealtimeMessaging.Abstractions.Interface.Notification;
using RealtimeMessaging.SignalR.Dispatchers;

namespace RealtimeMessaging.SignalR.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRealtimeMessageSignalR(
            this IServiceCollection services)
        {
            services.AddSignalRCore();
            
            services.AddSingleton<
                INotificationDispatcher,
                NotificationDispater>();

            return services;
        }
    }
}
