Public Interface IUserFactory
    Function Create() As IUser
    Function GetBySubscriberId(ByVal settings As ISettings, ByVal subscriberId As String) As IUser
    Function GetByEmailAddress(ByVal settings As ISettings, ByVal emailAddress As String) As IUser
End Interface
