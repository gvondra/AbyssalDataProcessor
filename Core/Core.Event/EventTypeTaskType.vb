Imports AbyssalDataProcessor.DataTier.Core.Models
Public Class EventTypeTaskType
    Implements IEventTypeTaskType

    Private m_eventTypeTaskTypeData As EventTypeTaskTypeData
    Private m_innerTaskType As ITaskType
    Private m_eventType As IEventType

    Friend Sub New(ByVal eventType As IEventType, ByVal taskType As ITaskType, ByVal data As EventTypeTaskTypeData)
        m_eventTypeTaskTypeData = data
        m_innerTaskType = taskType
        m_eventType = eventType
    End Sub

    Public Property IsActive As Boolean Implements IEventTypeTaskType.IsActive
        Get
            Return m_eventTypeTaskTypeData.IsActive
        End Get
        Set(value As Boolean)
            m_eventTypeTaskTypeData.IsActive = value
        End Set
    End Property

    Public ReadOnly Property TaskTypeId As Guid Implements ITaskType.TaskTypeId
        Get
            Return m_innerTaskType.TaskTypeId
        End Get
    End Property

    Public Property Title As String Implements ITaskType.Title
        Get
            Return m_innerTaskType.Title
        End Get
        Set(value As String)
            m_innerTaskType.Title = value
        End Set
    End Property

    Public Function CreateTaskTypeEventType(eventType As IEventType) As ITaskTypeEventType Implements ITaskType.CreateTaskTypeEventType
        Return m_innerTaskType.CreateTaskTypeEventType(eventType)
    End Function

    Public Function CreateTaskTypeGroup(group As IGroup) As ITaskTypeGroup Implements ITaskType.CreateTaskTypeGroup
        Return m_innerTaskType.CreateTaskTypeGroup(group)
    End Function

    Public Function GetDataCreator(settings As ISettings) As IDataCreator Implements ISavable.GetDataCreator
        Return m_innerTaskType.GetDataCreator(settings)
    End Function

    Public Function GetDataUpdater(settings As ISettings) As IDataUpdater Implements ISavable.GetDataUpdater
        Return m_innerTaskType.GetDataUpdater(settings)
    End Function

    Public Function GetEventTypes(settings As ISettings) As IEnumerable(Of ITaskTypeEventType) Implements ITaskType.GetEventTypes
        Return m_innerTaskType.GetEventTypes(settings)
    End Function

    Public Function GetGroups(settings As ISettings) As IEnumerable(Of ITaskTypeGroup) Implements ITaskType.GetGroups
        Return m_innerTaskType.GetGroups(settings)
    End Function
End Class
