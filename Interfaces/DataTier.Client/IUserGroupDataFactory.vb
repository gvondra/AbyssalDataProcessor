Public Interface IUserGroupDataFactory
    Function GetByUserId(ByVal settings As ISettings, ByVal organizationId As Guid, ByVal userId As Guid) As IEnumerable(Of UserGroupData)
End Interface
