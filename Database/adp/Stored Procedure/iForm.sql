﻿CREATE PROCEDURE [adp].[iForm]
	@id UNIQUEIDENTIFIER OUT, 
	@userId UNIQUEIDENTIFIER, 
    @formTypeId SMALLINT, 
    @style SMALLINT, 
    @content XML,	
	@timestamp DATETIME OUT
AS
BEGIN
	SET @id = NewId();
	SET @timestamp = GetDate();
	INSERT INTO [adp].[Form] ([FormId], [UserId], [FormTypeId], [Style], [Content], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @userId, @formTypeId, @style, @content, @timestamp, @timestamp);
END
