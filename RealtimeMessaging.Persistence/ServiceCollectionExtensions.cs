

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RealtimeMessaging.AspNetCore;
using RealtimeMessaging.Core.Repositories;
using RealtimeMessaging.Persistence.Configuration;
using RealtimeMessaging.Persistence.DbContexts;
using RealtimeMessaging.Persistence.Repositories;

namespace RealtimeMessaging.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRealtimeNotificationsPersistence(
        this IServiceCollection services,
        Action<NotificationPersistenceOptions> configure)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            var options = new NotificationPersistenceOptions();
            configure(options);

            services.AddDbContext<RealtimeNotificationsDbContext>(x =>
            {
                x.UseSqlServer(options.ConnectionString);
            });

            services.AddSingleton<DatabaseInstaller>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<NotificationOptions>>().Value;

                return new DatabaseInstaller(
                    options.PersistenceOptions.ConnectionString);
            });

            return services;
        }
    }
    public static class MigrationExtensions
    {
        public static async Task MigrateRealtimeNotifications(
            this IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var db = scope.ServiceProvider
                .GetRequiredService<DatabaseInstaller>();

            await db.InstallAsync();
        }
    }
}
