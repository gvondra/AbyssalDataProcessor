Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class TaskFactory
    Implements ITaskFactory

    Private m_userFactory As IUserFactory
    Private m_container As IContainer

    Public Sub New(ByVal userFactory As IUserFactory)
        m_userFactory = userFactory
        m_container = ObjectContainer.GetContainer
    End Sub

    Public Function Create(taskType As ITaskType) As ITask Implements ITaskFactory.Create
        Return New Task(m_userFactory, taskType)
    End Function

    Public Function [Get](settings As ISettings, taskId As Guid) As ITask Implements ITaskFactory.Get
        Dim factory As ITaskDataFactory
        Dim data As TaskData
        Dim result As Task = Nothing
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            factory = scope.Resolve(Of ITaskDataFactory)()
            data = factory.Get(New Settings(settings), taskId)
            If data IsNot Nothing Then
                result = New Task(m_userFactory, data)
            End If
        End Using
        Return result
    End Function
End Class
