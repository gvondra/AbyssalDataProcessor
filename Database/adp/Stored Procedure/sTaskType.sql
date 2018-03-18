CREATE PROCEDURE [adp].[sTaskType]
	@id UNIQUEIDENTIFIER
AS
SELECT [TaskTypeId], [Title], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[TaskType]
WHERE [TaskTypeId] = @id;