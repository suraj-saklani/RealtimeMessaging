using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeMessaging.Abstractions.Model.Notification
{
    public class RealtimeMessagingOptions
    {
        public string HubRoute { get; set; } = "/notifications";

        public Func<MessageReceivedContext, Task>? TokenResolver { get; set; }
    }
}
