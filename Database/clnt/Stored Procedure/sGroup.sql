CREATE PROCEDURE [clnt].[sGroup]
	@id UNIQUEIDENTIFIER,
	@organizationId UNIQUEIDENTIFIER
AS
SELECT [GroupId], [OrganizationId], [Name], [CreateTimestamp], [UpdateTimestamp]
FROM [clnt].[Group]
WHERE [GroupId] = @id
	AND [OrganizationId] = @organizationId
;