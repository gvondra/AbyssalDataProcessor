CREATE PROCEDURE [adp].[iEvent]
	@id UNIQUEIDENTIFIER OUT, 
    @eventTypeId SMALLINT, 
	@timestamp DATETIME OUT
AS
BEGIN
	SET @id = NewId();
	SET @timestamp = GetDate();
	INSERT INTO [adp].[Event] ([EventId], [EventTypeId], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @eventTypeId, @timestamp, @timestamp);
END