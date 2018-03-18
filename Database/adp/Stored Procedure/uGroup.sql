CREATE PROCEDURE [adp].[uGroup]
	@id UNIQUEIDENTIFIER, 
    @name NVARCHAR(100),
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	UPDATE [adp].[Group]
	SET [Name] = @name,
		[UpdateTimestamp] = @timestamp
	WHERE [GroupId] = @id;
END