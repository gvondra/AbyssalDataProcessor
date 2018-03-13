Imports AbyssalDataProcessor.DataTier.Core.Models
Public Class EventType
    Implements IEventType

    Private m_eventTypeData As EventTypeData

    Friend Sub New(ByVal eventTypeData As EventTypeData)
        m_eventTypeData = eventTypeData
    End Sub

    Public Property Title As String Implements IEventType.Title
        Get
            Return m_eventTypeData.Title
        End Get
        Set(value As String)
            m_eventTypeData.Title = value
        End Set
    End Property

    Public ReadOnly Property Type As Int16 Implements IEventType.Type
        Get
            Return m_eventTypeData.EventTypeId
        End Get
    End Property
End Class
