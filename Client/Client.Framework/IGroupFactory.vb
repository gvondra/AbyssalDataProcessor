Public Interface IGroupFactory
    Function Create(ByVal organziationId As Guid) As IGroup
    Function [Get](ByVal settings As ISettings, ByVal groupId As Guid, ByVal organizationId As Guid) As IGroup
    Function GetAll(ByVal settings As ISettings, ByVal organziationId As Guid) As IEnumerable(Of IGroup)
End Interface
