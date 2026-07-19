using Microsoft.EntityFrameworkCore;
using RealtimeMessaging.Abstractions.Entities.Notifiacton;
using RealtimeMessaging.Abstractions.Model.Entities.Chat;

namespace RealtimeMessaging.Persistence.DbContexts
{
    internal class RealtimeNotificationsDbContext : DbContext
    {
        public RealtimeNotificationsDbContext( DbContextOptions<RealtimeNotificationsDbContext> options) : base(options)
        {
        }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<ChatMessageEntity> ChatMessages { get; set; }
        public DbSet<ChatParticipantEntity> ChatParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region notification
            modelBuilder.Entity<NotificationEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NotifiedTo).HasMaxLength(450);
                entity.Property(e => e.Title).HasMaxLength(500);
                entity.Property(e => e.Type).HasMaxLength(100);

                entity.HasIndex(e => e.NotifiedTo);
                entity.HasIndex(e => e.IsRead);
                entity.HasIndex(e => new { e.NotifiedTo, e.IsRead });
                entity.HasIndex(e => e.CreatedAt);
                entity.HasIndex(e => new { e.NotifiedToType, e.NotifiedTo });
            });
            #endregion

            #region chat
            modelBuilder.Entity<ChatEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LastMessagePreview).HasMaxLength(500);
                entity.Property(e => e.LastMessageSenderId).HasMaxLength(450);
            });
            #endregion

            #region chat message
            modelBuilder.Entity<ChatMessageEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SenderId).HasMaxLength(450);
                
                entity.HasIndex(e => e.ChatId);
                entity.HasIndex(e => e.SenderId);
                entity.HasIndex(e => e.CreatedAt);
            });
            #endregion

            #region chat participant
            modelBuilder.Entity<ChatParticipantEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).HasMaxLength(450);
                entity.Property(e => e.Role).HasMaxLength(100);
                
                entity.HasIndex(e => e.ChatId);
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => new { e.ChatId, e.UserId });
            });
            #endregion
        }

    }
}
