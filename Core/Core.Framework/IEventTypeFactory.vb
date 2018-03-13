Public Interface IEventTypeFactory
    Function [Get](ByVal settings As ISettings, ByVal type As enumEventType) As IEventType
End Interface
