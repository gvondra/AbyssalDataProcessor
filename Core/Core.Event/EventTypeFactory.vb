Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class EventTypeFactory
    Implements IEventTypeFactory

    Private m_container As IContainer
    Private m_eventTypeSaver As IEventTypeSaver

    Public Sub New(ByVal eventTypeSaver As IEventTypeSaver)
        m_container = ObjectContainer.GetContainer
        m_eventTypeSaver = eventTypeSaver
    End Sub

    Public Sub New(ByVal container As IContainer, ByVal eventTypeSaver As IEventTypeSaver)
        m_container = container
        m_eventTypeSaver = eventTypeSaver
    End Sub

    Public Function [Get](settings As Framework.ISettings, type As enumEventType) As IEventType Implements IEventTypeFactory.Get
        Dim data As EventTypeData
        Dim dataFactory As IEventTypeDataFactory
        Dim blnSave As Boolean = False
        Dim result As EventType

        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            dataFactory = scope.Resolve(Of IEventTypeDataFactory)()
            data = dataFactory.Get(New Settings(settings), CType(type, Int16))
            If data Is Nothing Then
                data = New EventTypeData() With {.EventTypeId = CType(type, Int16), .Title = type.ToString}
                blnSave = True
            End If
        End Using
        result = New EventType(data)

        If blnSave Then
            m_eventTypeSaver.Create(settings, result)
        End If
        Return result
    End Function
End Class
