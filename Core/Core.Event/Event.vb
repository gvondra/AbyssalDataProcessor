Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class [Event]
    Implements IEvent

    Private m_eventData As EventData
    Private m_eventType As IEventType
    Private m_container As IContainer

    Friend Sub New(ByVal eventType As IEventType)
        m_eventType = eventType
        m_eventData = New EventData() With {.EventTypeId = eventType.Type}
        m_container = ObjectContainer.GetContainer
    End Sub

    Public ReadOnly Property EventId As Guid Implements IEvent.EventId
        Get
            Return m_eventData.EventId
        End Get
    End Property

    Public ReadOnly Property Type As enumEventType Implements IEvent.Type
        Get
            Return CType(m_eventData.EventTypeId, enumEventType)
        End Get
    End Property

    Public Function GetDataCreator(settings As Framework.ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataCreatorWrapper(scope.Resolve(Of EventDataSaver)(New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)), New TypedParameter(GetType(EventData), m_eventData)))
        End Using
    End Function

    Public Function GetDataUpdater(settings As Framework.ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Throw New NotImplementedException()
    End Function
End Class
