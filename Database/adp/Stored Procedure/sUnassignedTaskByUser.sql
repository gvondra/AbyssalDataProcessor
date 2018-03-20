CREATE PROCEDURE [adp].[sUnassignedTaskByUser]
	@userId UNIQUEIDENTIFIER
AS
SELECT [TaskId], [TaskTypeId], [Message], [CreateTimestamp], [UpdateTimestamp],
		[TaskTypeTitle],
		[GroupId], [GroupName]
FROM [adp].[UnassignedTask]
WHERE [GroupId] is Null
	OR [GroupId] in (
		SELECT [UG].[GroupId]
		FROM [adp].[UserGroup] [UG]
		WHERE [UG].[UserId] = @userId
	)
ORDER BY [UpdateTimestamp]
;