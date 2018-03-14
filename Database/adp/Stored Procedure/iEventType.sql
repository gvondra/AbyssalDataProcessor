CREATE PROCEDURE [adp].[iEventType]
	@id SMALLINT,
	@title NVARCHAR(250),
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	INSERT INTO [adp].[EventType] ([EventTypeId], [Title], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @title, @timestamp, @timestamp);
END