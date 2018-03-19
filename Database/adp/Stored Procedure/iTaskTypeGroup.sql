CREATE PROCEDURE [adp].[iTaskTypeGroup]
	@taskTypeId UNIQUEIDENTIFIER,
	@groupId UNIQUEIDENTIFIER,
	@isActive BIT,
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	INSERT INTO [adp].[TaskTypeGroup] ([TaskTypeId], [GroupId], [IsActive], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@taskTypeId, @groupId, @isActive, @timestamp, @timestamp);
END