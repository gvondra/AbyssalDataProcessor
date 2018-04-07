CREATE PROCEDURE [clnt].[sUserSearch]
	@value NVARCHAR(500),
	@wildCardValue NVARCHAR(500)
AS
SELECT [UserId], [OrganizationId], [FullName], [ShortName], [BirthDate], [EmailAddress], [PhoneNumber], [CreateTimestamp], [UpdateTimestamp]
FROM [clnt].[User]
WHERE [EmailAddress] = @value
	OR [FullName] LIKE @wildCardValue ESCAPE '\'
	OR [ShortName] LIKE @wildCardValue ESCAPE '\';