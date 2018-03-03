Public Interface IUserFactory
    Function GetBySubscriberId(ByVal settings As ISettings, ByVal subscriberId As String) As IUser
End Interface
