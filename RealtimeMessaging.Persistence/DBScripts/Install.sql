IF OBJECT_ID('dbo.Notifications', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Notifications
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

    CREATE INDEX IX_Notifications_UserId
        ON dbo.Notifications(UserId);

    CREATE INDEX IX_Notifications_IsRead
        ON dbo.Notifications(IsRead);

    CREATE INDEX IX_Notifications_UserId_IsRead
        ON dbo.Notifications(UserId, IsRead);

    CREATE INDEX IX_Notifications_CreatedAt
        ON dbo.Notifications(CreatedAt);
END;