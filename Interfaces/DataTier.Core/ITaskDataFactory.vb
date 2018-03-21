Public Interface ITaskDataFactory
    Function [Get](ByVal settings As ISettings, ByVal taskId As Guid) As TaskData
End Interface
