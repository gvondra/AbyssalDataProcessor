CREATE PROCEDURE [adp].[sEventTypeTaskTypeByEventTypeId]
	@eventTypeId SMALLINT
AS
BEGIN
	SELECT [EventTypeId], [TaskTypeId], [IsActive], [CreateTimestamp], [UpdateTimestamp]
	FROM [adp].[EventTypeTaskType]
	WHERE [EventTypeId] = @eventTypeId;

	SELECT [TT].[TaskTypeId], [TT].[Title], [TT].[CreateTimestamp], [TT].[UpdateTimestamp]
	FROM [adp].[TaskType] [TT]
		INNER JOIN [adp].[EventTypeTaskType] [ETTT] on [TT].[TaskTypeId] = [ETTT].[TaskTypeId]
	WHERE [ETTT].[EventTypeId] = @eventTypeId;
END
