Public Interface IGroupDataFactory
    Function [Get](ByVal settings As ISettings, ByVal groupId As Guid, ByVal organizationId As Guid) As GroupData
    Function GetAll(ByVal settings As ISettings, ByVal organizationId As Guid) As IEnumerable(Of GroupData)
End Interface
