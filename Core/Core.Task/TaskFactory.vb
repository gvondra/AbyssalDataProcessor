Public Class TaskFactory
    Implements ITaskFactory

    Public Function Create(taskType As ITaskType) As ITask Implements ITaskFactory.Create
        Return New Task(taskType)
    End Function
End Class
