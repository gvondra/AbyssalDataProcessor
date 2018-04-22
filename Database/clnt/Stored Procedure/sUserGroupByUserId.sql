CREATE PROCEDURE [clnt].[sUserGroupByUserId]
	@userId UNIQUEIDENTIFIER,
	@organizationId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT [UserId], [GroupId], [OrganizationId], [IsActive], [CreateTimestamp], [UpdateTimestamp]
	FROM [clnt].[UserGroup]
	WHERE [UserId] = @userId
		AND [OrganizationId] = @organizationId;

	SELECT [Gr].[GroupId], [Gr].[Name], [Gr].[CreateTimestamp], [Gr].[UpdateTimestamp]
	FROM [clnt].[Group] [Gr]
		INNER JOIN [clnt].[UserGroup] [UG] on [Gr].[GroupId] = [UG].[GroupId]
	WHERE [UG].[UserId] = @userId
		AND [UG].[OrganizationId] = @organizationId;
END
