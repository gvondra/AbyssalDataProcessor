CREATE PROCEDURE [clnt].[uUser]
	@id UNIQUEIDENTIFIER, 
	@organizationId UNIQUEIDENTIFIER,
    @fullName NVARCHAR(100), 
    @shortName NVARCHAR(80), 
    @birthDate DATE, 
    @emailAddress NVARCHAR(500), 
    @phoneNumber CHAR(13), 
	@timestamp DATETIME OUT
AS
BEGIN
	SET @timestamp = GetDate();
	UPDATE [clnt].[User]
	SET [FullName] = @fullName, 
		[ShortName] = @shortName, 
		[BirthDate] = @birthDate, 
		[EmailAddress] = @emailAddress, 
		[PhoneNumber] = @phoneNumber, 
		[UpdateTimestamp] = @timestamp
	WHERE [UserId] = @id
		AND [OrganizationId] = @organizationId;
END