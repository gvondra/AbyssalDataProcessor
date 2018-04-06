CREATE TABLE [clnt].[Form]
(
	[FormId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[OrganizationId] UNIQUEIDENTIFIER NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [FormTypeId] SMALLINT NOT NULL, 
    [Style] SMALLINT NOT NULL, 
    [Content] TEXT NULL,	
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [FK_Form_To_User] FOREIGN KEY ([UserId]) REFERENCES [clnt].[User]([UserId])
)

GO

CREATE INDEX [IX_Form_UserId] ON [clnt].[Form] ([UserId])
