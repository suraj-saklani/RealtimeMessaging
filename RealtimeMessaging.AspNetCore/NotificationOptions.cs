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
        internal bool PersistenceEnabled { get; set; }

        internal NotificationPersistenceOptions? PersistenceOptions { get; set; }

        public void UseSqlServer(
            string connectionString)
        {
            PersistenceEnabled = true;

            PersistenceOptions = new NotificationPersistenceOptions
            {
                ConnectionString = connectionString
            };
        }
    }
}
