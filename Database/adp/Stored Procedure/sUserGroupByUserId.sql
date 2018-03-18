CREATE PROCEDURE [adp].[sUserGroupByUserId]
	@userId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT [UserId], [GroupId], [IsActive], [CreateTimestamp], [UpdateTimestamp]
	FROM [adp].[UserGroup]
	WHERE [UserId] = @userId;

	SELECT [Gr].[GroupId], [Gr].[Name], [Gr].[CreateTimestamp], [Gr].[UpdateTimestamp]
	FROM [adp].[Group] [Gr]
		INNER JOIN [adp].[UserGroup] [UG] on [Gr].[GroupId] = [UG].[GroupId]
	WHERE [UG].[UserId] = @userId;
END
