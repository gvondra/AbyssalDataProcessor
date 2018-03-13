CREATE TABLE [adp].[WebMetricAttribute]
(
	[WebMetricAttributeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [WebMetricId] INT NOT NULL, 
    [Key] NVARCHAR(500) NOT NULL, 
    [Value] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_WebMetricAttribute_To_WebMetric] FOREIGN KEY ([WebMetricId]) REFERENCES [adp].[WebMetric]([WebMetricId])
)

GO

CREATE INDEX [IX_WebMetricAttribute_WebMetricId] ON [adp].[WebMetricAttribute] ([WebMetricId])
