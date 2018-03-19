CREATE PROCEDURE [adp].[iTask]
	@id UNIQUEIDENTIFIER OUT, 
	@taskTypeId UNIQUEIDENTIFIER,
	@timestamp DATETIME OUT	
AS
BEGIN
	SET @id = NewId();
	SET @timestamp = GetDate();
	INSERT INTO [adp].[Task] ([TaskId], [TaskTypeId], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @taskTypeId, @timestamp, @timestamp);
END