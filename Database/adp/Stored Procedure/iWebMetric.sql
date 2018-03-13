CREATE PROCEDURE [adp].[iWebMetric]
	@id INT OUT, 
    @url NVARCHAR(500), 
    @method NVARCHAR(50),
    @createTimestamp DATETIME, 
    @duration FLOAT, 
    @status NVARCHAR(50), 
    @controller NVARCHAR(50)
AS
BEGIN
	INSERT INTO [adp].[WebMetric] ([Url], [Method], [CreateTimestamp], [Duration], [Status], [Controller])
	VALUES (@url, @method, @createTimestamp, @duration, @status, @controller);
	SET @id = SCOPE_IDENTITY();
END
