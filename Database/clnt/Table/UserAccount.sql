CREATE TABLE [clnt].[UserAccount]
(
	[UserAccountId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [OrganizationId] UNIQUEIDENTIFIER NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [SubscriberId] NVARCHAR(1000) NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [FK_UserAccount_To_User] FOREIGN KEY ([UserId]) REFERENCES [clnt].[User]([UserId])
)

GO

CREATE UNIQUE INDEX [IX_UserAccount_SubscriberId] ON [clnt].[UserAccount] ([SubscriberId])

GO

CREATE INDEX [IX_UserAccount_UserId] ON [clnt].[UserAccount] ([UserId])
