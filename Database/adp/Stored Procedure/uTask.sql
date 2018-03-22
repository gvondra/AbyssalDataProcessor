CREATE PROCEDURE [adp].[uTask]
	@id UNIQUEIDENTIFIER, 
	@userId UNIQUEIDENTIFIER,
	@message NVARCHAR(MAX),
	@isClosed BIT,
	@timestamp DATETIME OUT	
AS
BEGIN
	SET @timestamp = GetDate();
	UPDATE [adp].[Task] 
	SET [UserId] = @userId,
		[Message] = @message,
		[isClosed] = @isClosed,
		[UpdateTimestamp] = @timestamp
	WHERE [TaskId] = @id;
END