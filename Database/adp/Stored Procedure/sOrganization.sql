CREATE PROCEDURE [adp].[sOrganization]
	@id UNIQUEIDENTIFIER
AS
SELECT [OrganizationId], [Name], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[Organization]
WHERE [OrganizationId] = @id;