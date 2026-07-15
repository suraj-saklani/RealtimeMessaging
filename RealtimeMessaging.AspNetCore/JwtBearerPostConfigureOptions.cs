using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using RealtimeMessaging.Abstractions.Model.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeMessaging.AspNetCore
{
    internal sealed class JwtBearerPostConfigureOptions
    : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly RealtimeMessagingOptions _options;

        public JwtBearerPostConfigureOptions(
            IOptions<RealtimeMessagingOptions> options)
        {
            _options = options.Value;
        }

        public void PostConfigure(string? name, JwtBearerOptions options)
        {
            var previous = options.Events.OnMessageReceived;

            options.Events.OnMessageReceived = async context =>
            {
                if (previous != null)
                {
                    await previous(context);
                }

                if (!string.IsNullOrEmpty(context.Token))
                {
                    return;
                }

                if (!context.HttpContext.Request.Path.StartsWithSegments(_options.HubRoute))
                {
                    return;
                }

                var token = context.Request.Query["access_token"];

                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
            };
        }
    }
}
