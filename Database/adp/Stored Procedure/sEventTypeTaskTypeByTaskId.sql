CREATE PROCEDURE [adp].[sEventTypeTaskTypeByTaskId]
	@taskTypeId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT [EventTypeId], [TaskTypeId], [IsActive], [CreateTimestamp], [UpdateTimestamp]
	FROM [adp].[EventTypeTaskType]
	WHERE [TaskTypeId] = @taskTypeId;

	SELECT [ET].[EventTypeId], [ET].[Title], [ET].[CreateTimestamp], [ET].[UpdateTimestamp]
	FROM [adp].[EventType] [ET]
		INNER JOIN [adp].[EventTypeTaskType] [ETTT] on [ET].[EventTypeId] = [ETTT].[EventTypeId]
	WHERE [ETTT].[TaskTypeId] = @taskTypeId;
END
