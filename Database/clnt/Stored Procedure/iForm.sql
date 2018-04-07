CREATE PROCEDURE [clnt].[iForm]
	@id UNIQUEIDENTIFIER OUT, 
	@userId UNIQUEIDENTIFIER, 
	@organizationId UNIQUEIDENTIFIER, 
    @formTypeId SMALLINT, 
    @style SMALLINT, 
    @content TEXT,	
	@timestamp DATETIME OUT
AS
BEGIN
	SET @id = NewId();
	SET @timestamp = GetDate();
	INSERT INTO [clnt].[Form] ([FormId], [UserId], [OrganizationId], [FormTypeId], [Style], [Content], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @userId, @organizationId, @formTypeId, @style, @content, @timestamp, @timestamp);
END
