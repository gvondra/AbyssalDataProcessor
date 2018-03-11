CREATE TABLE [adp].[FormTypeTaskType]
(
	[FormTypeId] SMALLINT NOT NULL , 
    [TaskTypeId] UNIQUEIDENTIFIER NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    PRIMARY KEY ([FormTypeId], [TaskTypeId]), 
    CONSTRAINT [FK_FormTypeTaskType_To_FormType] FOREIGN KEY ([FormTypeId]) REFERENCES [adp].[FormType]([FormTypeId]), 
    CONSTRAINT [FK_FormTypeTaskType_To_TaskType] FOREIGN KEY ([TaskTypeId]) REFERENCES [adp].[TaskType]([TaskTypeId])
)

GO

CREATE INDEX [IX_FormTypeTaskType_FormTypeId] ON [adp].[FormTypeTaskType] ([FormTypeId])

GO

CREATE INDEX [IX_FormTypeTaskType_TaskTypeId] ON [adp].[FormTypeTaskType] ([TaskTypeId])
