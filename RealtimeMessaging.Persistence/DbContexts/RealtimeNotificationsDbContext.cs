using Microsoft.EntityFrameworkCore;
using RealtimeMessaging.Abstractions.Entities.Notifiacton;

namespace RealtimeMessaging.Persistence.DbContexts
{
    public class RealtimeNotificationsDbContext : DbContext
    {
        public RealtimeNotificationsDbContext( DbContextOptions<RealtimeNotificationsDbContext> options) : base(options)
        {
        }
        public DbSet<NotificationEntity> Notifications => Set<NotificationEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(RealtimeNotificationsDbContext).Assembly);
        }

    }
}
