Public Interface IEventSaver
    Sub Create(ByVal settings As ISettings, ByVal request As ICreateEventRequest)

    Interface ICreateEventRequest
        Property [Event] As IEvent
        Property Forms As IEnumerable(Of IForm)
    End Interface
End Interface
