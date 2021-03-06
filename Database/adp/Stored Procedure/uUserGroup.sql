﻿CREATE PROCEDURE [adp].[uUserGroup]
	@userId UNIQUEIDENTIFIER,
	@groupId UNIQUEIDENTIFIER,
	@isActive BIT,
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	UPDATE [adp].[UserGroup]
	SET [IsActive] = @isActive,
		[UpdateTimestamp] = @timestamp
	WHERE [UserId] = @userId
		AND [GroupId] = @groupId
END
