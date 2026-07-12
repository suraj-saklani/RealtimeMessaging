using Microsoft.Extensions.DependencyInjection;
using RealtimeMessaging.Core;
using RealtimeMessaging.Core.Interface.Repositories;
using RealtimeMessaging.Core.Repositories;
using RealtimeMessaging.Persistence;
using RealtimeMessaging.SignalR.Extensions;

namespace RealtimeMessaging.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRealtimeNotifications(
        this IServiceCollection services, Action<NotificationOptions> configure)
        {
            services.AddRealtimeMessageSignalR();
            services.AddRealtimeNotificationsCore();

            var notificationOptions = new NotificationOptions();
            configure(notificationOptions);

            if (notificationOptions.PersistenceEnabled)
            {
                services.AddRealtimeNotificationsPersistence(options =>
                {
                    options.ConnectionString = notificationOptions.PersistenceOptions!.ConnectionString;
                });
            }
            else
            {
                services.AddScoped(typeof(IRepository<>), typeof(NullRepository<>));
            }

            return services;
        }
    }
}
