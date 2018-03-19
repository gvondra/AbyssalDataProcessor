Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class Task
    Implements ITask

    Private m_taskData As TaskData
    Private m_taskType As ITaskType
    Private m_container As IContainer

    Friend Sub New(ByVal taskData As TaskData)
        m_taskData = taskData
        m_container = ObjectContainer.GetContainer()
    End Sub

    Friend Sub New(ByVal taskType As ITaskType)
        m_taskData = New TaskData
        m_taskType = taskType
        m_container = ObjectContainer.GetContainer()
    End Sub

    Public ReadOnly Property TaskId As Guid Implements ITask.TaskId
        Get
            Return m_taskData.TaskId
        End Get
    End Property

    Public Function GetDataCreator(settings As ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Return New DataCreatorWrapper(Sub() Create(settings))
    End Function

    Private Sub Create(ByVal settings As ISettings)
        Dim creator As IDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            m_taskData.TaskTypeId = m_taskType.TaskTypeId
            creator = scope.Resolve(Of TaskDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)),
                New TypedParameter(GetType(TaskData), m_taskData)
            )
            creator.Create()
        End Using
    End Sub

    Public Function GetDataUpdater(settings As ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Throw New NotImplementedException()
    End Function
End Class
