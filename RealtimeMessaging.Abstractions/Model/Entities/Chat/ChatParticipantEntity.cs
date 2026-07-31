using RealtimeMessaging.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeMessaging.Abstractions.Model.Entities.Chat
{
    public class ChatParticipantEntity: BaseEntity
    {
        public Guid ChatId { get; set; }
        public string UserId { get; set; }
        public RoleEnum? Role { get; set; } // e.g., "admin", "member", etc.
        public DateTime JoinedAt { get; set; }
    }
    public enum RoleEnum
    {
        Admin,
        Member,
        Guest
    }
}
