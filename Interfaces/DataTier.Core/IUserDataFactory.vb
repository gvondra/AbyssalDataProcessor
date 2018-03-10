Public Interface IUserDataFactory
    Function GetBySubscriberId(ByVal settings As ISettings, ByVal subscriberId As String) As UserData
    Function GetByEmailAddress(ByVal settings As ISettings, ByVal emailAddress As String) As IEnumerable(Of UserData)
    Function GetAccountCount(ByVal settings As ISettings) As Integer
End Interface
