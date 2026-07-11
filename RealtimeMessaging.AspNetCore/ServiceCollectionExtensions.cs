using Microsoft.Extensions.DependencyInjection;
using RealtimeMessaging.SignalR.Extensions;
using RealtimeMessaging.Core.Service.NotificationService;
using RealtimeMessaging.Abstractions.Interface.Notification;

namespace RealtimeMessaging.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRealtimeNotifications(
        this IServiceCollection services)
        {
            services.AddRealtimeMessageSignalR();

            services.AddScoped<
                INotificationService,
                NotificationService>();
            
            return services;
        }
    }
}
