CREATE PROCEDURE [clnt].[iUserAccount]
	@id UNIQUEIDENTIFIER OUT, 
	@organizationId UNIQUEIDENTIFIER,
    @userId UNIQUEIDENTIFIER, 
    @subscriberId NVARCHAR(1000), 
    @timestamp DATETIME OUT
AS
BEGIN
	SET @id = NewId();
	SET @timestamp = GetDate();
	INSERT INTO [clnt].[UserAccount] ([UserAccountId], [UserId], [OrganizationId], [SubscriberId], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @userId, @organizationId, @subscriberId, @timestamp, @timestamp);
END