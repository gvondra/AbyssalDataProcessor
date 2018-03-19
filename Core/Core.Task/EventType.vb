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

    Public ReadOnly Property EventTypeId As Int16 Implements IEventType.EventTypeId
        Get
            Return m_eventTypeData.EventTypeId
        End Get
    End Property

    Public Function GetDataCreator(settings As Framework.ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Throw New NotImplementedException()
    End Function

    Public Function GetDataUpdater(settings As Framework.ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Throw New NotImplementedException()
    End Function
End Class
