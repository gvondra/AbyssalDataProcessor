CREATE PROCEDURE [adp].[sEventTypeTaskTypeAll]
AS
SELECT [EventTypeId], [TaskTypeId], [IsActive], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[EventTypeTaskType];
