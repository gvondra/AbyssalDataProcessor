CREATE PROCEDURE [adp].[sUser]
	@userId UNIQUEIDENTIFIER
AS
SELECT [UserId], [FullName], [ShortName], [BirthDate], [EmailAddress], [PhoneNumber], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[User]
WHERE [UserId] = @userId;
