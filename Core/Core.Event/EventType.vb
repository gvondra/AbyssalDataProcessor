Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class EventType
    Implements IEventType

    Private m_eventTypeData As EventTypeData
    Private m_taskTypeFactory As ITaskTypeFactory
    Private m_container As IContainer

    Friend Sub New(ByVal eventTypeData As EventTypeData, ByVal taskTypeFactory As ITaskTypeFactory)
        m_eventTypeData = eventTypeData
        m_taskTypeFactory = taskTypeFactory
        m_container = ObjectContainer.GetContainer
    End Sub

    Friend Sub New(ByVal container As IContainer, ByVal eventTypeData As EventTypeData)
        m_eventTypeData = eventTypeData
        m_container = container
    End Sub

    Public Property Title As String Implements IEventType.Title
        Get
            Return m_eventTypeData.Title
        End Get
        Set(value As String)
            m_eventTypeData.Title = value
        End Set
    End Property

    Public ReadOnly Property EventTypeId As Int16 Implements IEventType.EventTypeId
        Get
            Return m_eventTypeData.EventTypeId
        End Get
    End Property

    Public Function GetDataCreator(settings As Framework.ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataCreatorWrapper(scope.Resolve(Of EventTypeDataSaver)(New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)), New TypedParameter(GetType(EventTypeData), m_eventTypeData)))
        End Using
    End Function

    Public Function GetDataUpdater(settings As Framework.ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataUpdateWrapper(scope.Resolve(Of EventTypeDataSaver)(New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)), New TypedParameter(GetType(EventTypeData), m_eventTypeData)))
        End Using
    End Function

    Public Function GetTaskTypes(settings As ISettings) As IEnumerable(Of IEventTypeTaskType) Implements IEventType.GetTaskTypes
        Dim factory As IEventTypeTaskTypeDataFactory
        Dim result As IEnumerable(Of IEventTypeTaskType)
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            factory = scope.Resolve(Of IEventTypeTaskTypeDataFactory)()
            result = factory.GetByEventTypeId(New Settings(settings), EventTypeId).AsParallel _
                        .Where(Function(d As EventTypeTaskTypeData) d.IsActive) _
                        .Select(Of IEventTypeTaskType)(Function(d As EventTypeTaskTypeData) LoadTaskType(settings, d))
        End Using
        Return result
    End Function

    Private Function LoadTaskType(settings As Framework.ISettings, d As EventTypeTaskTypeData) As IEventTypeTaskType
        Return New EventTypeTaskType(Me, m_taskTypeFactory.Get(settings, d.TaskTypeId), d)
    End Function
End Class
