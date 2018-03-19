Public Interface ITaskFactory
    Function Create(ByVal taskType As ITaskType) As ITask
End Interface
