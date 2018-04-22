Public Class GroupDataFactory
    Implements IGroupDataFactory

    Public Property GenericDataFactory As IGenericDataFactory(Of GroupData)

    Public Sub New()
        Me.GenericDataFactory = New GenericDataFactory(Of GroupData)()
    End Sub

    Public Function [Get](ByVal settings As ISettings, groupId As Guid, organizationId As Guid) As GroupData Implements IGroupDataFactory.Get
        Return Me.Get(settings, New DbProviderFactory(), groupId, organizationId)
    End Function

    Public Function [Get](ByVal settings As ISettings, ByVal providerFactory As IDbProviderFactory, groupId As Guid, organizationId As Guid) As GroupData
        Dim parameter As IDbDataParameter = CreateParameter(providerFactory, "id", DbType.Guid)
        Dim orgIdParam As IDbDataParameter = CreateParameter(providerFactory, "organizationId", DbType.Guid)
        orgIdParam.Value = organizationId
        parameter.Value = groupId
        Return Me.GenericDataFactory.GetData(settings,
                                             providerFactory,
                                             "clnt.sGroup",
                                             Function() New GroupData,
                                             New Action(Of IEnumerable(Of GroupData))(AddressOf AssignDataStateManager(Of GroupData)),
                                             {parameter, orgIdParam}).FirstOrDefault
    End Function

    Public Function GetAll(ByVal settings As ISettings, organizationId As Guid) As IEnumerable(Of GroupData) Implements IGroupDataFactory.GetAll
        Return Me.GetAll(settings, New DbProviderFactory, organizationId)
    End Function

    Public Function GetAll(ByVal settings As ISettings, ByVal providerFactory As IDbProviderFactory, organizationId As Guid) As IEnumerable(Of GroupData)
        Dim orgIdParam As IDbDataParameter = CreateParameter(providerFactory, "organizationId", DbType.Guid)
        orgIdParam.Value = organizationId
        Return Me.GenericDataFactory.GetData(settings,
                                             providerFactory,
                                             "clnt.sGroupAll",
                                             Function() New GroupData,
                                             New Action(Of IEnumerable(Of GroupData))(AddressOf AssignDataStateManager(Of GroupData)),
                                             {orgIdParam})
    End Function
End Class
