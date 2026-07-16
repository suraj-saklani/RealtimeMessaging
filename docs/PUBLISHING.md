# Publishing RealtimeMessaging.AspNetCore

`RealtimeMessaging.AspNetCore` is the public package consumers should install. It currently has project dependencies on `RealtimeMessaging.Core`, `RealtimeMessaging.Persistence`, and `RealtimeMessaging.SignalR`.

NuGet restores those implementation packages transitively. Therefore, publish all four packages to the same NuGet feed, even though only `RealtimeMessaging.AspNetCore` is documented for consumer use.

## Before the first public release

1. Choose and add a repository license. Then set `PackageLicenseExpression` in `RealtimeMessaging.AspNetCore.csproj` (for example, `MIT`) or use `PackageLicenseFile` for a custom license.
2. Confirm that the package ID `RealtimeMessaging.AspNetCore` is available on NuGet.org.
3. Add tests for broadcast, user, group, JWT connection, and persistence behavior.
4. Set a new package version for every release. Never overwrite an existing NuGet version.
5. Build and pack from a clean checkout.

## Build and package

```powershell
dotnet restore RealtimeMessaging.web/RealtimeMessaging.web.slnx
dotnet build RealtimeMessaging.web/RealtimeMessaging.web.slnx -c Release --no-restore

dotnet pack RealtimeMessaging.Abstractions/RealtimeMessaging.Abstractions.csproj -c Release --no-build
dotnet pack RealtimeMessaging.Core/RealtimeMessaging.Core.csproj -c Release --no-build
dotnet pack RealtimeMessaging.SignalR/RealtimeMessaging.SignalR.csproj -c Release --no-build
dotnet pack RealtimeMessaging.Persistence/RealtimeMessaging.Persistence.csproj -c Release --no-build
dotnet pack RealtimeMessaging.AspNetCore/RealtimeMessaging.AspNetCore.csproj -c Release --no-build
```

## Publish

Publish the dependency packages first, then publish `RealtimeMessaging.AspNetCore`.

```powershell
dotnet nuget push "RealtimeMessaging.Abstractions/bin/Release/*.nupkg" --source https://api.nuget.org/v3/index.json --api-key <NUGET_API_KEY>
dotnet nuget push "RealtimeMessaging.Core/bin/Release/*.nupkg" --source https://api.nuget.org/v3/index.json --api-key <NUGET_API_KEY>
dotnet nuget push "RealtimeMessaging.SignalR/bin/Release/*.nupkg" --source https://api.nuget.org/v3/index.json --api-key <NUGET_API_KEY>
dotnet nuget push "RealtimeMessaging.Persistence/bin/Release/*.nupkg" --source https://api.nuget.org/v3/index.json --api-key <NUGET_API_KEY>
dotnet nuget push "RealtimeMessaging.AspNetCore/bin/Release/*.nupkg" --source https://api.nuget.org/v3/index.json --api-key <NUGET_API_KEY>
```

Do not commit or place the NuGet API key in source code, `appsettings.json`, or documentation.

## Verify before publishing

Create a new empty ASP.NET Core 8 project, add only `RealtimeMessaging.AspNetCore`, and verify that restore, build, `AddRealtimeNotifications()`, and `MapRealtimeNotifications()` all work. This catches missing transitive package dependencies.
