CREATE PROCEDURE [adp].[sUserSearch]
	@value NVARCHAR(500),
	@wildCardValue NVARCHAR(500)
AS
SELECT [UserId], [FullName], [ShortName], [BirthDate], [EmailAddress], [PhoneNumber], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[User]
WHERE [EmailAddress] = @value
	OR [FullName] LIKE @wildCardValue ESCAPE '\'
	OR [ShortName] LIKE @wildCardValue ESCAPE '\';