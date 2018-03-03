Public Class UserDataFactory
    Implements IUserDataFactory

    Public Property GenericDataFactory As IGenericDataFactory(Of UserData)

    Public Sub New()
        Me.GenericDataFactory = New GenericDataFactory(Of UserData)()
    End Sub

    Public Function GetByEmailAddress(settings As ISettings, emailAddress As String) As IEnumerable(Of UserData) Implements IUserDataFactory.GetByEmailAddress
        Return GetByEmailAddress(settings, New DbProviderFactory(), emailAddress)
    End Function

    Public Function GetByEmailAddress(settings As ISettings, ByVal providerFactory As IDbProviderFactory, emailAddress As String) As IEnumerable(Of UserData)
        Dim parameter As IDbDataParameter = CreateParameter(providerFactory, "emailAddress", DbType.String)
        parameter.Value = emailAddress
        Return Me.GenericDataFactory.GetData(settings, providerFactory, "adp.sUserByEmailAddress", Function() New UserData, {parameter})
    End Function

    Public Function GetBySubscriberId(settings As ISettings, subscriberId As String) As UserData Implements IUserDataFactory.GetBySubscriberId
        Return GetBySubscriberId(settings, New DbProviderFactory(), subscriberId)
    End Function

    Public Function GetBySubscriberId(settings As ISettings, ByVal providerFactory As IDbProviderFactory, subscriberId As String) As UserData
        Dim parameter As IDbDataParameter = CreateParameter(providerFactory, "subscriberId", DbType.String)
        parameter.Value = subscriberId
        Return Me.GenericDataFactory.GetData(settings, providerFactory, "adp.sUserBySubscriberId", Function() New UserData, {parameter}).FirstOrDefault
    End Function
End Class
