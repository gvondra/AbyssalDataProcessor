Public Interface IUserDataFactory
    Function GetBySubscriberId(ByVal settings As ISettings, ByVal subscriberId As String) As UserData
    Function GetByEmailAddress(ByVal settings As ISettings, ByVal emailAddress As String) As IList(Of UserData)
End Interface
