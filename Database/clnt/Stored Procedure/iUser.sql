CREATE PROCEDURE [clnt].[iUser]
	@id UNIQUEIDENTIFIER OUT, 
	@organizationId UNIQUEIDENTIFIER,
    @fullName NVARCHAR(100), 
    @shortName NVARCHAR(80), 
    @birthDate DATE, 
    @emailAddress NVARCHAR(500), 
    @phoneNumber CHAR(13), 
	@timestamp DATETIME OUT
AS
BEGIN
	SET @id = NewId();
	SET @timestamp = GetDate();
	INSERT INTO [clnt].[User] ([UserId], [OrganizationId], [FullName], [ShortName], [BirthDate], [EmailAddress], [PhoneNumber], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @organizationId, @fullName, @shortName, @birthDate, @emailAddress, @phoneNumber, @timestamp, @timestamp);
END
