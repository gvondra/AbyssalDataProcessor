Public Class UserDataSaver
    Implements IUserDataCreator

    Private m_userData As UserData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal userData As UserData)
        m_settings = settings
        m_userData = userData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory)
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Create(providerFactory, Nothing)
    End Sub

    Public Sub Create(userAccount As UserAccountData) Implements IUserDataCreator.Create
        Create(New DbProviderFactory, userAccount)
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory, userAccount As UserAccountData)
        Dim userId As IDbDataParameter
        Dim userTimestamp As IDbDataParameter
        Dim accountId As IDbDataParameter = Nothing
        Dim accountTimestamp As IDbDataParameter = Nothing
        Dim name As String

        providerFactory.EstablishTransaction(m_settings)
        Using command As IDbCommand = m_settings.Connection.CreateCommand
            command.Transaction = m_settings.Transaction
            command.CommandType = CommandType.StoredProcedure
            If userAccount IsNot Nothing Then
                command.CommandText = "adp.iUserAndAccount"
            Else
                command.CommandText = "adp.iUser"
            End If

            If userAccount IsNot Nothing Then
                name = "userId"
            Else
                name = "id"
            End If
            userId = CreateParameter(providerFactory, name, DbType.Guid)
            userId.Direction = ParameterDirection.Output
            command.Parameters.Add(userId)


            If userAccount IsNot Nothing Then
                name = "userTimestamp"
            Else
                name = "timestamp"
            End If
            userTimestamp = CreateParameter(providerFactory, name, DbType.DateTime)
            userTimestamp.Direction = ParameterDirection.Output
            command.Parameters.Add(userTimestamp)

            If userAccount IsNot Nothing Then
                accountId = CreateParameter(providerFactory, "userAccountId", DbType.Guid)
                accountId.Direction = ParameterDirection.Output
                command.Parameters.Add(accountId)


                accountTimestamp = CreateParameter(providerFactory, "accountTimestamp", DbType.DateTime)
                accountTimestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(accountTimestamp)
            End If

            AddParameter(providerFactory, command.Parameters, "fullName", DbType.String, GetParameterValue(m_userData.FullName))
            AddParameter(providerFactory, command.Parameters, "shortName", DbType.String, GetParameterValue(m_userData.ShortName))
            AddParameter(providerFactory, command.Parameters, "birthDate", DbType.Date, GetParameterValue(m_userData.BirthDate))
            AddParameter(providerFactory, command.Parameters, "emailAddress", DbType.String, GetParameterValue(m_userData.EmailAddress))
            AddParameter(providerFactory, command.Parameters, "phoneNumber", DbType.AnsiString, GetParameterValue(m_userData.PhoneNumber))


            If userAccount IsNot Nothing Then
                AddParameter(providerFactory, command.Parameters, "subscriberId", DbType.String, GetParameterValue(userAccount.SubscriberId))
            End If

            command.ExecuteNonQuery()
            m_userData.UserId = CType(userId.Value, Guid)
            m_userData.CreateTimestamp = CType(userTimestamp.Value, Date)
            m_userData.UpdateTimestamp = CType(userTimestamp.Value, Date)
            If userAccount IsNot Nothing Then
                userAccount.UserId = CType(accountId.Value, Guid)
                userAccount.CreateTimestamp = CType(accountTimestamp.Value, Date)
                userAccount.UpdateTimestamp = CType(accountTimestamp.Value, Date)
            End If
        End Using
    End Sub
End Class
