Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class EventType
    Implements IEventType

    Private m_eventTypeData As EventTypeData
    Private m_container As IContainer

    Friend Sub New(ByVal eventTypeData As EventTypeData)
        m_eventTypeData = eventTypeData
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
End Class
