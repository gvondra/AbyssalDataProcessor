CREATE PROCEDURE [adp].[uOrganization]
	@id UNIQUEIDENTIFIER, 
    @name NVARCHAR(500),
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	UPDATE [adp].[Organization]
	SET [Name] = @name,
		[UpdateTimestamp] = @timestamp
	WHERE [OrganizationId] = @id;
END