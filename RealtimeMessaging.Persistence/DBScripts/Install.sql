IF OBJECT_ID('dbo.Notifications', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Notifications
    (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

        NotifiedTo NVARCHAR(450) NOT NULL,
        
        NotifiedToType INT NOT NULL,

        Title NVARCHAR(500) NOT NULL,

        Message NVARCHAR(MAX) NOT NULL,

        Type NVARCHAR(100) NULL,

        Data NVARCHAR(MAX) NULL,

        IsRead BIT NOT NULL,

        ReadAt DATETIME2 NULL,

        CreatedAt DATETIME2 NOT NULL,

        ModifiedAt DATETIME2 NULL
    );

    CREATE INDEX IX_Notifications_NotifiedTo
        ON dbo.Notifications(NotifiedTo);

    CREATE INDEX IX_Notifications_IsRead
        ON dbo.Notifications(IsRead);

    CREATE INDEX IX_Notifications_NotifiedTo_IsRead
        ON dbo.Notifications(NotifiedTo, IsRead);

    CREATE INDEX IX_Notifications_CreatedAt
        ON dbo.Notifications(CreatedAt);
    
    CREATE INDEX IX_Notifications_NotifiedToType_NotifiedTo
        ON dbo.Notifications(NotifiedToType, NotifiedTo);
END;