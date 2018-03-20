CREATE PROCEDURE [adp].[uTask]
	@id UNIQUEIDENTIFIER, 
	@userId UNIQUEIDENTIFIER,
	@message NVARCHAR(MAX),
	@timestamp DATETIME OUT	
AS
BEGIN
	SET @timestamp = GetDate();
	UPDATE [adp].[Task] 
	SET [UserId] = @userId,
		[Message] = @message,
		[UpdateTimestamp] = @timestamp
	WHERE [TaskId] = @id;
END