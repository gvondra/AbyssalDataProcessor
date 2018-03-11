CREATE PROCEDURE [adp].[sForm]
	@formId UNIQUEIDENTIFIER
AS
SELECT [FormId], [UserId], [FormTypeId], [Style], [Content], [CreateTimestamp], [UpdateTimestamp]
FROM [adp].[Form]
WHERE [FormId] = @formId
;