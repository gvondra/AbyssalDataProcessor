Public Interface ITask
    Inherits ISavable

    ReadOnly Property TaskId As Guid
    Property Message As String
End Interface
