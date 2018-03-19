CREATE PROCEDURE [adp].[iEventTask]
	@eventId UNIQUEIDENTIFIER, 
	@taskId UNIQUEIDENTIFIER,
	@timestamp DATETIME OUT	
AS
BEGIN
	SET @timestamp = GetDate();
	INSERT INTO [adp].[EventTask] ([EventId], [TaskId], [CreateTimestamp], [UpdateTimestamp]) 
	VALUES (@eventId, @taskId, @timestamp, @timestamp);
END
