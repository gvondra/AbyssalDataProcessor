CREATE PROCEDURE [adp].[sTaskFormByTaskId]
	@taskId UNIQUEIDENTIFIER
AS
SELECT [FormId]
FROM [adp].[TaskForm]
WHERE [TaskId] = @taskId;
