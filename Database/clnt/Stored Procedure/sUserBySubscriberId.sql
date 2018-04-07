CREATE PROCEDURE [clnt].[sUserBySubscriberId]
	@subscriberId NVARCHAR(1000)
AS
	SELECT [User].[UserId], [User].[OrganizationId], [User].[FullName], [User].[ShortName], [User].[BirthDate], [User].[EmailAddress], [User].[PhoneNumber], [User].[CreateTimestamp], [User].[UpdateTimestamp]
	FROM [clnt].[User]
		INNER JOIN [adp].[UserAccount] on [User].[UserId] = [UserAccount].[UserId]
	WHERE [UserAccount].[SubscriberId] = @subscriberId;
