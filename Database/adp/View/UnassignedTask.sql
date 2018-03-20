CREATE VIEW [adp].[UnassignedTask]
	AS SELECT [Task].[TaskId], [Task].[TaskTypeId], [Task].[Message], [Task].[CreateTimestamp], [Task].[UpdateTimestamp],
		[TT].[Title] [TaskTypeTitle],
		[Group].[GroupId], COALESCE([Group].[Name], 'Ungrouped') [GroupName]
	FROM [adp].[Task]
		INNER JOIN [adp].[TaskType] [TT] on [Task].[TaskTypeId] = [TT].[TaskTypeId]
		LEFT JOIN (
			[adp].[TaskTypeGroup] [TTG]
			INNER JOIN [adp].[Group] on [TTG].IsActive = 1 AND [TTG].[GroupId] = [Group].[GroupId]
		) ON [TT].[TaskTypeId] = [TTG].[TaskTypeId]
	WHERE [Task].[UserId] is Null;
