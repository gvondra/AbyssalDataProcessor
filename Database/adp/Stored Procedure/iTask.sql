CREATE PROCEDURE [adp].[iTask]
	@id UNIQUEIDENTIFIER OUT, 
	@taskTypeId UNIQUEIDENTIFIER,
	@userId UNIQUEIDENTIFIER,
	@message NVARCHAR(MAX),
	@timestamp DATETIME OUT	
AS
BEGIN
	SET @id = NewId();
	SET @timestamp = GetDate();
	INSERT INTO [adp].[Task] ([TaskId], [TaskTypeId], [UserId], [Message], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @taskTypeId, @userId, @message, @timestamp, @timestamp);
END