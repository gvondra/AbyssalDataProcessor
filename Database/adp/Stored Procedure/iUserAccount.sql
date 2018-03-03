CREATE PROCEDURE [adp].[iUserAccount]
	@id UNIQUEIDENTIFIER OUT, 
    @userId UNIQUEIDENTIFIER, 
    @subscriberId NVARCHAR(1000), 
    @timestamp DATETIME OUT
AS
BEGIN
	SET @id = NewId();
	SET @timestamp = GetDate();
	INSERT INTO [adp].[UserAccount] ([UserAccountId], [UserId], [SubscriberId], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @userId, @subscriberId, @timestamp, @timestamp);
END