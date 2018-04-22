CREATE PROCEDURE [clnt].[uGroup]
	@id UNIQUEIDENTIFIER, 
	@organizationId UNIQUEIDENTIFIER,
    @name NVARCHAR(100),
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	UPDATE [clnt].[Group]
	SET [Name] = @name,
		[UpdateTimestamp] = @timestamp
	WHERE [GroupId] = @id
		AND [OrganizationId] = @organizationId;
END