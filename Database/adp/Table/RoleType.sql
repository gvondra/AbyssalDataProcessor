CREATE TABLE [adp].[RoleType]
(
	[RoleTypeId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Code] CHAR(10) NOT NULL,
    [Title] NVARCHAR(100) NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate()
)

GO

CREATE UNIQUE INDEX [IX_RoleType_Code] ON [adp].[RoleType] ([Code])
