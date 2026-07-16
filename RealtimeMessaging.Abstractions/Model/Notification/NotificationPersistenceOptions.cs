namespace RealtimeMessaging.Persistence.Configuration
{
    public class NotificationPersistenceOptions
    {
        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets a value indicating whether to automatically migrate the database.
        /// </summary>
        public bool AutoMigration { get; set; } = false;

    }

}
