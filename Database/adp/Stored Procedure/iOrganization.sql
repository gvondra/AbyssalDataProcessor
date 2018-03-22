CREATE PROCEDURE [adp].[iOrganization]
	@id UNIQUEIDENTIFIER OUT, 
    @name NVARCHAR(500),
	@timestamp DATETIME OUT
AS
BEGIN
	SET @id = NewId();
	SET @timestamp = GetDate();
	INSERT INTO [adp].[Organization] ([OrganizationId], [Name], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @name, @timestamp, @timestamp);
END