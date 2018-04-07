Imports System.Text.RegularExpressions
Public Class UserDataFactory
    Implements IUserDataFactory

    Public Property GenericDataFactory As IGenericDataFactory(Of UserData)

    Public Sub New()
        Me.GenericDataFactory = New GenericDataFactory(Of UserData)()
    End Sub

    Public Function GetBySubscriberId(settings As ISettings, ByVal organizationId As Guid, subscriberId As String) As UserData Implements IUserDataFactory.GetBySubscriberId
        Return GetBySubscriberId(settings, New DbProviderFactory(), organizationId, subscriberId)
    End Function

    Public Function GetBySubscriberId(settings As ISettings, ByVal providerFactory As IDbProviderFactory, ByVal organizationId As Guid, subscriberId As String) As UserData
        Dim parameter As IDbDataParameter = CreateParameter(providerFactory, "subscriberId", DbType.String)
        parameter.Value = subscriberId
        Dim organizationParameter As IDbDataParameter = CreateParameter(providerFactory, "organizationId", DbType.Guid)
        organizationParameter.Value = organizationId
        Return Me.GenericDataFactory.GetData(settings,
                                             providerFactory,
                                             "clnt.sUserBySubscriberId",
                                             Function() New UserData,
                                             New Action(Of IEnumerable(Of UserData))(AddressOf AssignDataStateManager(Of UserData)),
                                             {parameter, organizationParameter}).FirstOrDefault
    End Function

    Public Function [Get](settings As ISettings, ByVal organizationId As Guid, userId As Guid) As UserData Implements IUserDataFactory.Get
        Return Me.Get(settings, New DbProviderFactory, organizationId, userId)
    End Function

    Public Function [Get](settings As ISettings, ByVal providerFactory As IDbProviderFactory, ByVal organizationId As Guid, userId As Guid) As UserData
        Dim parameter As IDbDataParameter = CreateParameter(providerFactory, "userId", DbType.Guid)
        parameter.Value = userId
        Dim organizationParameter As IDbDataParameter = CreateParameter(providerFactory, "organizationId", DbType.Guid)
        organizationParameter.Value = organizationParameter
        Return Me.GenericDataFactory.GetData(settings,
                                             providerFactory,
                                             "clnt.sUser",
                                             Function() New UserData,
                                             New Action(Of IEnumerable(Of UserData))(AddressOf AssignDataStateManager(Of UserData)),
                                             {parameter, organizationParameter}).FirstOrDefault
    End Function

    Public Function Search(settings As ISettings, ByVal organizationId As Guid, searchText As String) As IEnumerable(Of UserData) Implements IUserDataFactory.Search
        Return Search(settings, New DbProviderFactory(), organizationId, searchText)
    End Function

    Public Function Search(settings As ISettings, ByVal providerFactory As IDbProviderFactory, ByVal organizationId As Guid, searchText As String) As IEnumerable(Of UserData)
        Dim organizationParameter As IDbDataParameter = CreateParameter(providerFactory, "organizationId", DbType.Guid)
        organizationParameter.Value = organizationParameter
        searchText = searchText.Trim
        Dim value As IDbDataParameter = CreateParameter(providerFactory, "value", DbType.String)
        value.Value = searchText
        Dim wildCardValue As IDbDataParameter = CreateParameter(providerFactory, "wildCardValue", DbType.String)
        searchText = Regex.Replace(searchText, "\\", "\\")
        searchText = Regex.Replace(searchText, "_", "\_")
        searchText = Regex.Replace(searchText, "%", "\%")
        searchText = Regex.Replace(searchText, "\s+", "%")
        wildCardValue.Value = "%" & searchText & "%"
        Return Me.GenericDataFactory.GetData(settings,
                                             providerFactory,
                                             "clnt.sUserSearch",
                                             Function() New UserData,
                                             New Action(Of IEnumerable(Of UserData))(AddressOf AssignDataStateManager(Of UserData)),
                                             {value, wildCardValue, organizationParameter})
    End Function
End Class
