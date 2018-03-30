CREATE TABLE [clnt].[TaskType]
(
	[TaskTypeId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [OrganizationId] UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR(100) NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate()
)
