using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RealtimeMessaging.Abstractions.Model.Notification;
using RealtimeMessaging.Core;
using RealtimeMessaging.Core.Interface.Repositories;
using RealtimeMessaging.Core.Repositories;
using RealtimeMessaging.Persistence;
using RealtimeMessaging.SignalR.Extensions;
using RealtimeMessaging.SignalR.Hubs;

namespace RealtimeMessaging.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRealtimeNotifications(
            this IServiceCollection services,
            Action<NotificationOptions>? configure = null)
        {
            // Create a single options instance
            var notificationOptions = new NotificationOptions();

            // Apply consumer configuration
            configure?.Invoke(notificationOptions);

            // Register it so everyone gets the same instance
            services.AddSingleton<IOptions<NotificationOptions>>(
                Options.Create(notificationOptions));

            // Register library services
            services.AddRealtimeMessageSignalR();
            services.AddRealtimeNotificationsCore();

            // JWT integration
            services.AddSingleton<
                IPostConfigureOptions<JwtBearerOptions>,
                JwtBearerPostConfigureOptions>();

            // Persistence
            if (notificationOptions.PersistenceEnabled)
            {
                services.AddRealtimeNotificationsPersistence(options =>
                {
                    options.ConnectionString =
                        notificationOptions.PersistenceOptions!.ConnectionString;
                });
            }
            else
            {
                services.AddScoped(typeof(IRepository<>), typeof(NullRepository<>));
            }

            return services;
        }

        public static IEndpointRouteBuilder MapRealtimeNotifications(
            this IEndpointRouteBuilder endpoints)
        {
            var options = endpoints.ServiceProvider
                .GetRequiredService<IOptions<NotificationOptions>>()
                .Value;

            endpoints.MapHub<NotificationHub>(
                options.RealtimeMessagingOptions.HubRoute);

            return endpoints;
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRealtimeNotifications(
            this IApplicationBuilder app)
        {
            var options = app.ApplicationServices
                .GetRequiredService<IOptions<NotificationOptions>>()
                .Value;

            if (options.PersistenceEnabled &&
                options.PersistenceOptions?.AutoMigration == true)
            {
                app.ApplicationServices.MigrateRealtimeNotifications();
            }

            return app;
        }
    }
}