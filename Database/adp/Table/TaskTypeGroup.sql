CREATE TABLE [adp].[TaskTypeGroup]
(
	[TaskTypeId] UNIQUEIDENTIFIER NOT NULL , 
    [GroupId] UNIQUEIDENTIFIER NOT NULL,
    [IsActive] BIT NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(),
    PRIMARY KEY ([GroupId], [TaskTypeId]), 
    CONSTRAINT [FK_TaskTypeGroup_To_TaskType] FOREIGN KEY ([TaskTypeId]) REFERENCES [adp].[TaskType]([TaskTypeId]), 
    CONSTRAINT [FK_TaskTypeGroup_To_Group] FOREIGN KEY ([GroupId]) REFERENCES [adp].[Group]([GroupId])
)

GO

CREATE INDEX [IX_TaskTypeGroup_TaskTypeId] ON [adp].[TaskTypeGroup] ([TaskTypeId])

GO

CREATE INDEX [IX_TaskTypeGroup_GroupId] ON [adp].[TaskTypeGroup] ([GroupId])
