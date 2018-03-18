Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports autofac
Public Class TaskType
    Implements ITaskType

    Private m_taskTypeData As TaskTypeData
    Private m_container As IContainer

    Friend Sub New(ByVal taskTypeData As TaskTypeData)
        m_taskTypeData = taskTypeData
        m_container = ObjectContainer.GetContainer()
    End Sub

    Public ReadOnly Property TaskTypeId As Guid Implements ITaskType.TaskTypeId
        Get
            Return m_taskTypeData.TaskTypeId
        End Get
    End Property

    Public Property Title As String Implements ITaskType.Title
        Get
            Return m_taskTypeData.Title
        End Get
        Set(value As String)
            m_taskTypeData.Title = value
        End Set
    End Property

    Public Function GetDataCreator(settings As ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataCreatorWrapper(scope.Resolve(Of TaskTypeDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings),
                New Settings(settings)), New TypedParameter(GetType(TaskTypeData), m_taskTypeData)
            ))
        End Using
    End Function

    Public Function GetDataUpdater(settings As ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataUpdateWrapper(scope.Resolve(Of TaskTypeDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings),
                New Settings(settings)), New TypedParameter(GetType(TaskTypeData), m_taskTypeData)
            ))
        End Using
    End Function
End Class
