CREATE PROCEDURE [adp].[sEventTypeAll]
AS
SELECT [EventTypeId], [Title], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[EventType];
