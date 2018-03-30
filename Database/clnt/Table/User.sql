CREATE TABLE [clnt].[User]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [OrganizationId] UNIQUEIDENTIFIER NOT NULL,
    [FullName] NVARCHAR(100) NOT NULL, 
    [ShortName] NVARCHAR(100) NOT NULL, 
    [BirthDate] DATE NULL, 
    [EmailAddress] NVARCHAR(500) NOT NULL, 
    [PhoneNumber] CHAR(13) NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate()
)
