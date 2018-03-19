CREATE TABLE [adp].[Task]
(
	[TaskId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [TaskTypeId] UNIQUEIDENTIFIER NOT NULL,
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [FK_Task_To_TaskType] FOREIGN KEY ([TaskTypeId]) REFERENCES [adp].[TaskType]([TaskTypeId])
)

GO

CREATE INDEX [IX_Task_TaskTypeId] ON [adp].[Task] ([TaskTypeId])
