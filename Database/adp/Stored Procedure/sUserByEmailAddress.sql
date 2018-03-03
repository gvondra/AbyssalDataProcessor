CREATE PROCEDURE [adp].[sUserByEmailAddress]
	@emailAddress NVARCHAR(500)
AS
	SELECT [UserId], [FullName], [ShortName], [BirthDate], [EmailAddress], [PhoneNumber], [CreateTimestamp], [UpdateTimestamp]
	FROM [adp].[User]
	WHERE [EmailAddress] = @emailAddress;
