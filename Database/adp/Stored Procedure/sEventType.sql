CREATE PROCEDURE [adp].[sEventType]
	@id SMALLINT
AS
SELECT [EventTypeId], [Title], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[EventType]
WHERE [EventTypeId] = @id;
