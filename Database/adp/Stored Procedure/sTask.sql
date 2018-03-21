CREATE PROCEDURE [adp].[sTask]
	@taskId as UNIQUEIDENTIFIER
AS
SELECT [TaskId], [TaskTypeId], [UserId], [Message], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[Task]
WHERE [TaskId] = @taskId
;
