IF OBJECT_ID('dbo.RealtimeNotifications', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.RealtimeNotifications
    (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

        UserId NVARCHAR(450) NOT NULL,

        Title NVARCHAR(500) NOT NULL,

        Message NVARCHAR(MAX) NOT NULL,

        Type NVARCHAR(100) NULL,

        Data NVARCHAR(MAX) NULL,

        IsRead BIT NOT NULL,

        ReadAt DATETIME2 NULL,

        CreatedAt DATETIME2 NOT NULL,

        ModifiedAt DATETIME2 NOT NULL
    );

    CREATE INDEX IX_RealtimeNotifications_UserId
        ON dbo.RealtimeNotifications(UserId);

    CREATE INDEX IX_RealtimeNotifications_IsRead
        ON dbo.RealtimeNotifications(IsRead);

    CREATE INDEX IX_RealtimeNotifications_UserId_IsRead
        ON dbo.RealtimeNotifications(UserId, IsRead);

    CREATE INDEX IX_RealtimeNotifications_CreatedAt
        ON dbo.RealtimeNotifications(CreatedAt);
END;