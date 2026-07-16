# RealtimeMessaging.AspNetCore

SignalR-based real-time notifications for ASP.NET Core 8 applications.

## Install

```bash
dotnet add package RealtimeMessaging.AspNetCore
```

## Configure the server

Register the notification services, then map the hub endpoint. The default hub route is `/notifications`.

```csharp
using RealtimeMessaging.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRealtimeNotifications(options =>
{
    options.RealtimeMessagingOptions.HubRoute = "/notifications";
});

var app = builder.Build();

app.MapRealtimeNotifications();

app.Run();
```

If the application uses JWT bearer authentication, configure it before mapping the hub and add the authentication middleware:

```csharp
builder.Services
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        // Configure the authority, audience, and token validation parameters.
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapRealtimeNotifications();
```

The package reads a SignalR access token from the `access_token` query-string parameter only for the configured hub route.

### Custom or encrypted SignalR tokens

`RealtimeMessagingOptions.TokenResolver` is intended for applications that use a custom query-string token format instead of SignalR's standard `access_token` parameter. For example, ABP-based applications can send an encrypted `enc_auth_token` and decrypt it before JWT bearer authentication validates the resulting JWT.

```csharp
builder.Services.AddRealtimeNotifications(options =>
{
    options.RealtimeMessagingOptions.TokenResolver = context =>
    {
        var encryptedToken = context.Request.Query["enc_auth_token"];

        if (!string.IsNullOrWhiteSpace(encryptedToken))
        {
            context.Token = SimpleStringCipher.Instance
                .Decrypt(encryptedToken.ToString());
        }

        return Task.CompletedTask;
    };
});
```

The resolver should only extract or decrypt the token. JWT bearer authentication remains responsible for validating its signature, expiry, issuer, and audience.

> **Implementation requirement:** The package's JWT post-configurer must invoke `RealtimeMessagingOptions.TokenResolver` for this configuration to take effect, then fall back to `access_token` when the resolver does not set `context.Token`.

## Send notifications

Inject `INotificationService` into an application service, controller, or endpoint handler.

```csharp
using RealtimeMessaging.Abstractions.Interface.Notification;
using RealtimeMessaging.Abstractions.Model.Notification;

public sealed class OrderNotifier(INotificationService notifications)
{
    public Task NotifyCreatedAsync(string userId) =>
        notifications.NotifyUserAsync(userId, new NotificationRequest
        {
            Title = "Order created",
            Message = "Your order was created successfully.",
            Type = "success"
        });
}
```

All delivery modes call the same client callback: `ReceiveMessage`.

```javascript
connection.on("ReceiveMessage", notification => {
  console.log(notification.title, notification.message);
});
```

## Optional SQL Server persistence

```csharp
builder.Services.AddRealtimeNotifications(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Notifications")!,
        autoMigration: true);
});
```

Pass `saveInDB: true` to a notification method to store that notification.

## Important behavior

- `SendToUserAsync` targets SignalR's user identifier. By default SignalR uses the authenticated user's `NameIdentifier` claim. Ensure that identifier matches the `userId` passed to the notification service.
- The package sends to a named SignalR group but does not currently assign connections to groups. Your application must provide that connection-to-group behavior before using group notifications.
- The package currently contains SQL Server persistence. This dependency is installed even if persistence is not enabled.

## Requirements

- .NET 8 or later
- An ASP.NET Core host
- A SignalR client that subscribes to `ReceiveMessage`

## Source and issues

Source code and issue tracking: https://github.com/suraj-saklani/RealtimeMessaging
