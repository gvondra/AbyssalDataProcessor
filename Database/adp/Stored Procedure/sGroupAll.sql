CREATE PROCEDURE [adp].[sGroupAll]
AS
SELECT [GroupId], [Name], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[Group]
;

