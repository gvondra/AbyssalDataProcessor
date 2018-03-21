Public Interface ITaskFactory
    Function Create(ByVal taskType As ITaskType) As ITask
    Function [Get](ByVal settings As ISettings, ByVal taskId As Guid) As ITask
End Interface
