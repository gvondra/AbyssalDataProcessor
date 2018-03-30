CREATE TABLE [clnt].[Event]
(
	[EventId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [OrganizationId] UNIQUEIDENTIFIER NOT NULL,
    [EventTypeId] SMALLINT NOT NULL, 
    [Message] NVARCHAR(MAX) NOT NULL DEFAULT (''), 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate()
)
