CREATE TABLE [adp].[UserGroup]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL , 
    [GroupId] UNIQUEIDENTIFIER NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(),
    PRIMARY KEY ([GroupId], [UserId]), 
    CONSTRAINT [FK_UserGroup_To_User] FOREIGN KEY ([UserId]) REFERENCES [adp].[User]([UserId]), 
    CONSTRAINT [FK_UserGroup_To_Group] FOREIGN KEY ([GroupId]) REFERENCES [adp].[Group]([GroupId])
)

GO

CREATE INDEX [IX_UserGroup_UserId] ON [adp].[UserGroup] ([UserId])

GO

CREATE INDEX [IX_UserGroup_GroupId] ON [adp].[UserGroup] ([GroupId])
