using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealtimeMessaging.Abstractions.Entities.Notifiacton;

namespace RealtimeMessaging.Persistence.Configuration.EnityDBConfiguratoin
{
    public sealed class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.ToTable("RealtimeNotifications");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Title)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Message)
                   .HasMaxLength(4000)
                   .IsRequired();

            builder.Property(x => x.Type)
                   .HasMaxLength(50);


            builder.Property(x => x.IsRead)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => new
            {
                x.UserId,
                x.IsRead
            });
            builder.HasIndex(x => x.CreatedAt);
        }
    }
}
