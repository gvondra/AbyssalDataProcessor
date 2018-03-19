CREATE PROCEDURE [adp].[sTaskTypeGroupByTaskTypeId]
	@taskTypeId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT [TaskTypeId], [GroupId], [IsActive], [CreateTimestamp], [UpdateTimestamp]
	FROM [adp].[TaskTypeGroup]
	WHERE [TaskTypeId] = @taskTypeId;

	SELECT [G].[GroupId], [G].[Name], [G].[CreateTimestamp], [G].[UpdateTimestamp]
	FROM [adp].[TaskTypeGroup] [TTG]
		INNER JOIN [adp].[Group] [G] on [TTG].[GroupId] = [G].[GroupId]
	WHERE [TTG].[TaskTypeId] = @taskTypeId;
END
