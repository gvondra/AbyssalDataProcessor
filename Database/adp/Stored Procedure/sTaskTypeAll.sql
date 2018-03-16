CREATE PROCEDURE [adp].[sTaskTypeAll]
AS
SELECT [TaskTypeId], [Title], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[TaskType];
