CREATE PROCEDURE [adp].[sGroup]
	@id UNIQUEIDENTIFIER
AS
SELECT [GroupId], [Name], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[Group]
WHERE [GroupId] = @id
;