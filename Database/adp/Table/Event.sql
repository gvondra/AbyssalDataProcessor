CREATE TABLE [adp].[Event]
(
	[EventId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [EventTypeId] SMALLINT NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate()
)
