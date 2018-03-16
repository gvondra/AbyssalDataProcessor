Public Interface IEvent
    Inherits ISavable

    ReadOnly Property EventId As Guid
    ReadOnly Property Type As enumEventType

    Function AddForm(ByVal form As IForm) As IForm
End Interface
