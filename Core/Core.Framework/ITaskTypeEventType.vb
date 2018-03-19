Public Interface ITaskTypeEventType
    Inherits IEventType
    Inherits ISavable

    Property IsActive As Boolean

    Sub Save(ByVal settings As ISettings)
End Interface
