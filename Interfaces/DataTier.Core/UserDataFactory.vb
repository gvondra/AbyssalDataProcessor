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

    Public Function GetAccountCount(settings As ISettings) As Int32 Implements IUserDataFactory.GetAccountCount
        Return GetAccountCount(settings, New DbProviderFactory())
    End Function

    Public Function GetAccountCount(settings As ISettings, ByVal providerFactory As IDbProviderFactory) As Int32
        Dim count As Integer = 0
        Using connection As IDbConnection = providerFactory.OpenConnection(settings.ConnectionString)
            Using command As IDbCommand = connection.CreateCommand
                command.CommandText = "adp.sUserAccountCount"
                command.CommandType = CommandType.StoredProcedure

                count = CType(command.ExecuteScalar(), Integer)
            End Using
            connection.Close()
        End Using
        Return count
    End Function
End Class
