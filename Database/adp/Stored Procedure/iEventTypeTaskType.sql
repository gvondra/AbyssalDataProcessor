CREATE PROCEDURE [adp].[iEventTypeTaskType]
	@eventTypeId SMALLINT,
	@taskTypeId UNIQUEIDENTIFIER,
	@isActive BIT,
	@timestamp DATETIME OUT	
AS
BEGIN
	SET @timestamp = GetDate();
	INSERT INTO [adp].[EventTypeTaskType]  ([EventTypeId], [TaskTypeId], [IsActive], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@eventTypeId, @taskTypeId, @isActive, @timestamp, @timestamp);
END
