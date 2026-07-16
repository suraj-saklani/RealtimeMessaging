using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RealtimeMessaging.Abstractions.Model.Notification
{
    public class RealtimeMessagingOptions
    {
        /// <summary>
        /// Hub route for the SignalR notifications hub. Default is "/notifications".
        /// </summary>
        public string HubRoute { get; set; } = "/notifications";

        /// <summary>
        /// Resolves the token from the incoming request.
        /// </summary>
        public Func<MessageReceivedContext, Task>? TokenResolver { get; set; }
    }
}
