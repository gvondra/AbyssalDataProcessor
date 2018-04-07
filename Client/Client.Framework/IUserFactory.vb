Public Interface IUserFactory
    Function Create() As IUser
    Function GetBySubscriberId(ByVal settings As ISettings, ByVal organizationId As Guid, ByVal subscriberId As String) As IUser
    Function [Get](ByVal settings As ISettings, ByVal organizationId As Guid, ByVal userId As Guid) As IUser
    Function Search(ByVal settings As ISettings, ByVal organizationId As Guid, ByVal searchText As String) As IEnumerable(Of IUser)
End Interface
