CREATE TABLE [clnt].[Group]
(
	[GroupId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [OrganizationId] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(100) NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate()
)
