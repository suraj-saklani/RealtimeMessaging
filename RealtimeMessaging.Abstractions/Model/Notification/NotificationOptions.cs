using RealtimeMessaging.Abstractions.Model.Notification;
using RealtimeMessaging.Persistence.Configuration;

namespace RealtimeMessaging.AspNetCore
{
    public class NotificationOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether persistence is enabled.
        /// </summary>
        public bool PersistenceEnabled { get; internal set; }
        /// <summary>
        /// Gets or sets the notification persistence options.
        /// </summary>
        public NotificationPersistenceOptions? PersistenceOptions { get; internal set; }
        /// <summary>
        /// Gets or sets the real-time messaging options.
        /// </summary>
        public RealtimeMessagingOptions RealtimeMessagingOptions { get; set; }

        public void UseSqlServer(
            string connectionString, bool autoMigration = false)
        {
            PersistenceEnabled = true;

            PersistenceOptions = new NotificationPersistenceOptions
            {
                ConnectionString = connectionString,
                AutoMigration = autoMigration
            };
        }
    }
}
