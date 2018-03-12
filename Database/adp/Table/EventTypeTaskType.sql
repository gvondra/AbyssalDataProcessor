CREATE TABLE [adp].[EventTypeTaskType]
(
	[EventTypeId] SMALLINT NOT NULL , 
    [TaskTypeId] UNIQUEIDENTIFIER NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    PRIMARY KEY ([EventTypeId], [TaskTypeId]), 
    CONSTRAINT [FK_EventTypeTaskType_To_FormType] FOREIGN KEY ([EventTypeId]) REFERENCES [adp].[EventType]([EventTypeId]), 
    CONSTRAINT [FK_EventTypeTaskType_To_TaskType] FOREIGN KEY ([TaskTypeId]) REFERENCES [adp].[TaskType]([TaskTypeId])
)

GO

CREATE INDEX [IX_EventTypeTaskType_FormTypeId] ON [adp].[EventTypeTaskType] ([EventTypeId])

GO

CREATE INDEX [IX_EventTypeTaskType_TaskTypeId] ON [adp].[EventTypeTaskType] ([TaskTypeId])
