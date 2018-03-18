Public Interface ITaskType
    Inherits ISavable

    ReadOnly Property TaskTypeId As Guid
    Property Title As String
End Interface
