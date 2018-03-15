CREATE PROCEDURE [adp].[iEventForm]
	@eventId UNIQUEIDENTIFIER,
	@formId UNIQUEIDENTIFIER,
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	INSERT INTO [adp].[EventForm] ([EventId], [FormId], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@eventId, @formId, @timestamp, @timestamp);
END