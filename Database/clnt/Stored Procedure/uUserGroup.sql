CREATE PROCEDURE [clnt].[uUserGroup]
	@userId UNIQUEIDENTIFIER,
	@groupId UNIQUEIDENTIFIER,
	@organizationId UNIQUEIDENTIFIER,
	@isActive BIT,
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	UPDATE [clnt].[UserGroup]
	SET [IsActive] = @isActive,
		[UpdateTimestamp] = @timestamp
	WHERE [UserId] = @userId
		AND [GroupId] = @groupId
		and [OrganizationId] = @organizationId
	;
END
