using RealtimeMessaging.Abstractions.Model.Notification;
using RealtimeMessaging.Persistence.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeMessaging.AspNetCore
{
    public class NotificationOptions
    {
        public bool PersistenceEnabled { get; internal set; }
        public NotificationPersistenceOptions? PersistenceOptions { get; internal set; }
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
