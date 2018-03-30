CREATE TABLE [clnt].[EventType]
(
	[EventTypeId] SMALLINT NOT NULL PRIMARY KEY, 
    [OrganizationId] UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR(250) NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate()
)
