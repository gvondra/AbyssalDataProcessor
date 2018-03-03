CREATE TABLE [adp].[UserOrganization]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL , 
    [OrganizationId] UNIQUEIDENTIFIER NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(),
    PRIMARY KEY ([OrganizationId], [UserId]), 
    CONSTRAINT [FK_UserOrganization_To_User] FOREIGN KEY ([UserId]) REFERENCES [adp].[User]([UserId]), 
    CONSTRAINT [FK_UserOrganization_To_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [adp].[Organization]([OrganizationId])
)

GO

CREATE INDEX [IX_UserOrganization_UserId] ON [adp].[UserOrganization] ([UserId])

GO

CREATE INDEX [IX_UserOrganization_OrganizationId] ON [adp].[UserOrganization] ([OrganizationId])
