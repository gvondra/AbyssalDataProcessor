Public Interface IUserDataFactory
    Function GetBySubscriberId(ByVal settings As ISettings, ByVal organizationId As Guid, ByVal subscriberId As String) As UserData
    Function [Get](ByVal settings As ISettings, ByVal organizationId As Guid, ByVal userId As Guid) As UserData
    Function Search(ByVal settings As ISettings, ByVal organizationId As Guid, ByVal searchText As String) As IEnumerable(Of UserData)
End Interface
