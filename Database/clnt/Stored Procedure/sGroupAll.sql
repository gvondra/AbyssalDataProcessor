CREATE PROCEDURE [clnt].[sGroupAll]
	@organizationId UNIQUEIDENTIFIER
AS
SELECT [GroupId], [OrganizationId], [Name], [CreateTimestamp], [UpdateTimestamp]
FROM [clnt].[Group]
WHERE [OrganizationId] = @organizationId
;

