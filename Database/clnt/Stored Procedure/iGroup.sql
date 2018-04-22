CREATE PROCEDURE [clnt].[iGroup]
	@id UNIQUEIDENTIFIER OUT, 
	@organizationId UNIQUEIDENTIFIER,
    @name NVARCHAR(100),
	@timestamp DATETIME OUT
AS
BEGIN
	SET @id = NewId();
	SET @timestamp = GetDate();
	INSERT INTO [clnt].[Group] ([GroupId], [OrganizationId], [Name], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @organizationId, @name, @timestamp, @timestamp);
END