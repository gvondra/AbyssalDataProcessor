CREATE TABLE [adp].[EventForm]
(
	[EventId] UNIQUEIDENTIFIER NOT NULL,
	[FormId] UNIQUEIDENTIFIER NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [PK_EventForm] PRIMARY KEY ([EventId], [FormId]), 
    CONSTRAINT [FK_EventForm_To_Event] FOREIGN KEY ([EventId]) REFERENCES [adp].[Event]([EventId]), 
    CONSTRAINT [FK_EventForm_To_Form] FOREIGN KEY ([FormId]) REFERENCES [adp].[Form]([FormId])
)

GO

CREATE INDEX [IX_EventForm_EventId] ON [adp].[EventForm] ([EventId])

GO

CREATE INDEX [IX_EventForm_FormId] ON [adp].[EventForm] ([FormId])
