namespace RealtimeMessaging.Persistence.Configuration
{
    public class NotificationPersistenceOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public bool AutoMigration { get; set; } = false;

    }

}
