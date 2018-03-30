CREATE TABLE [clnt].[TaskTypeGroup]
(
	[TaskTypeId] UNIQUEIDENTIFIER NOT NULL , 
    [GroupId] UNIQUEIDENTIFIER NOT NULL,
    [IsActive] BIT NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL DEFAULT GetDate(), 
    [UpdateTimestamp] DATETIME NOT NULL DEFAULT GetDate(),
    PRIMARY KEY ([GroupId], [TaskTypeId]), 
    CONSTRAINT [FK_TaskTypeGroup_To_TaskType] FOREIGN KEY ([TaskTypeId]) REFERENCES [clnt].[TaskType]([TaskTypeId]), 
    CONSTRAINT [FK_TaskTypeGroup_To_Group] FOREIGN KEY ([GroupId]) REFERENCES [clnt].[Group]([GroupId])
)

GO

CREATE INDEX [IX_TaskTypeGroup_TaskTypeId] ON [clnt].[TaskTypeGroup] ([TaskTypeId])

GO

CREATE INDEX [IX_TaskTypeGroup_GroupId] ON [clnt].[TaskTypeGroup] ([GroupId])
