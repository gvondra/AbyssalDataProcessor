CREATE PROCEDURE [adp].[sTaskByUserId]
	@userId UNIQUEIDENTIFIER
AS
SELECT [TaskId], [TaskTypeId], [UserId], [Message], [IsClosed], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[Task]
WHERE [UserId] = @userId
ORDER BY [CreateTimestamp]
;

