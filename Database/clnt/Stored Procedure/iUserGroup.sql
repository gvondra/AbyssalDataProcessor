CREATE PROCEDURE [clnt].[iUserGroup]
	@userId UNIQUEIDENTIFIER,
	@groupId UNIQUEIDENTIFIER,
	@organizationId UNIQUEIDENTIFIER,
	@isActive BIT,
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	INSERT INTO [clnt].[UserGroup] ([UserId], [GroupId], [OrganizationId], [IsActive], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@userId, @groupId, @organizationId, @isActive, @timestamp, @timestamp);
END