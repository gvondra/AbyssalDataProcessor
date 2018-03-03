CREATE TABLE [adp].[UserRole]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL , 
    [RoleTypeId] UNIQUEIDENTIFIER NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(),
    PRIMARY KEY ([RoleTypeId], [UserId]), 
    CONSTRAINT [FK_UserRole_To_User] FOREIGN KEY ([UserId]) REFERENCES [adp].[User]([UserId]), 
    CONSTRAINT [FK_UserRole_To_RoleType] FOREIGN KEY ([RoleTypeId]) REFERENCES [adp].[RoleType]([RoleTypeId])
)

GO

CREATE INDEX [IX_UserRole_UserId] ON [adp].[UserRole] ([UserId])

GO

CREATE INDEX [IX_UserRole_RoleTypeId] ON [adp].[UserRole] ([RoleTypeId])
