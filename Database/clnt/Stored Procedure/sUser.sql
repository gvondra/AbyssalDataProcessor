CREATE PROCEDURE [clnt].[sUser]
	@userId UNIQUEIDENTIFIER
AS
SELECT [UserId], [OrganizationId], [FullName], [ShortName], [BirthDate], [EmailAddress], [PhoneNumber], [CreateTimestamp], [UpdateTimestamp]
FROM [clnt].[User]
WHERE [UserId] = @userId;
