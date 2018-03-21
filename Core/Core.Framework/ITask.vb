Public Interface ITask
    Inherits ISavable

    ReadOnly Property TaskId As Guid
    Property Message As String

    Function GetUser(ByVal settings As ISettings) As IUser
    Sub SetUser(ByVal user As IUser)
End Interface
