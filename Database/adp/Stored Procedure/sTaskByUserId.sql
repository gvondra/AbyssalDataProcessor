CREATE PROCEDURE [adp].[sTaskByUserId]
	@userId UNIQUEIDENTIFIER
AS
SELECT [TaskId], [TaskTypeId], [UserId], [Message], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[Task]
WHERE [UserId] = @userId
;

