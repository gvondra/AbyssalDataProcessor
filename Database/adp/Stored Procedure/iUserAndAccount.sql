CREATE PROCEDURE [adp].[iUserAndAccount]
	@userId UNIQUEIDENTIFIER OUT, 
	@userAccountId UNIQUEIDENTIFIER OUT, 
    @fullName NVARCHAR(100), 
    @shortName NVARCHAR(80), 
    @birthDate DATE, 
    @emailAddress NVARCHAR(500), 
    @phoneNumber CHAR(13), 
    @subscriberId NVARCHAR(1000), 
	@userTimestamp DATETIME OUT, 
	@accountTimestamp DATETIME OUT
AS
BEGIN 
	EXECUTE [adp].[iUser] @userId, @fullName, @shortName, @birthDate, @emailAddress, @phoneNumber, @userTimestamp;
	EXECUTE [adp].[iUserAccount] @userAccountId, @userId, @subscriberId, @accountTimestamp;
END