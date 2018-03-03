Public Interface IUser
    ReadOnly Property UserId As Guid
    Property FullName As String
    Property ShortName As String
    Property BirthDate As Date?
    Property EmailAddress As String
    Property PhoneNumber As String
    ReadOnly Property CreateTimestamp As Date
    ReadOnly Property UpdateTimestamp As Date
End Interface
