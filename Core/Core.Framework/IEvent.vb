Public Interface IEvent
    Inherits ISavable

    ReadOnly Property EventId As Guid
    ReadOnly Property Type As enumEventType
End Interface
