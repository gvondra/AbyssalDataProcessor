Public Interface IUser
    Inherits ISavable

    ReadOnly Property UserId As Guid
    Property FullName As String
    Property ShortName As String
    Property BirthDate As Date?
    Property EmailAddress As String
    Property PhoneNumber As String
    ReadOnly Property CreateTimestamp As Date
    ReadOnly Property UpdateTimestamp As Date

    Function GetAccountDataCreater(ByVal settings As ISettings, ByVal subscriberId As String) As IDataCreator
    Function GetGroups(ByVal settings As ISettings) As IEnumerable(Of IUserGroup)
End Interface
