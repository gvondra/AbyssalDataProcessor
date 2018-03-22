CREATE PROCEDURE [adp].[sOrganizationSearch]
	@wildCardValue NVARCHAR(500)
AS
SELECT [OrganizationId], [Name], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[Organization]
WHERE [Name] LIKE @wildCardValue ESCAPE '\';