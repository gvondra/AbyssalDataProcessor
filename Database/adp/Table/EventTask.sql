CREATE TABLE [adp].[EventTask]
(
	[EventId] UNIQUEIDENTIFIER NOT NULL,
	[TaskId] UNIQUEIDENTIFIER NOT NULL,
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [PK_EventTask] PRIMARY KEY ([TaskId], [EventId]), 
    CONSTRAINT [FK_EventTask_To_Event] FOREIGN KEY ([EventId]) REFERENCES [adp].[Event]([EventId]), 
    CONSTRAINT [FK_EventTask_To_Task] FOREIGN KEY ([TaskId]) REFERENCES [adp].[Task]([TaskId])
)

GO

CREATE INDEX [IX_EventTask_EventId] ON [adp].[EventTask] ([EventId])

GO

CREATE INDEX [IX_EventTask_TaskId] ON [adp].[EventTask] ([TaskId])
