CREATE PROCEDURE [adp].[iUserGroup]
	@userId UNIQUEIDENTIFIER,
	@groupId UNIQUEIDENTIFIER,
	@isActive BIT,
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	INSERT INTO [adp].[UserGroup] ([UserId], [GroupId], [IsActive], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@userId, @groupId, @isActive, @timestamp, @timestamp);
END