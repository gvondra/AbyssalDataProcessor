Public Class UserAccountDataSaver
    Implements IDataCreator

    Private m_userAccountData As UserAccountData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal userAccountData As UserAccountData)
        m_settings = settings
        m_userAccountData = userAccountData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory())
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Dim id As IDbDataParameter
        Dim timestamp As IDbDataParameter

        providerFactory.EstablishTransaction(m_settings)
        Using command As IDbCommand = m_settings.Connection.CreateCommand
            command.Transaction = m_settings.Transaction
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "adp.iUserAccount"

            id = CreateParameter(providerFactory, "id", DbType.Guid)
            id.Direction = ParameterDirection.Output
            command.Parameters.Add(id)

            timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
            timestamp.Direction = ParameterDirection.Output
            command.Parameters.Add(timestamp)

            AddParameter(providerFactory, command.Parameters, "userId", DbType.Guid, GetParameterValue(m_userAccountData.UserId))
            AddParameter(providerFactory, command.Parameters, "subscriberId", DbType.String, GetParameterValue(m_userAccountData.SubscriberId))

            command.ExecuteNonQuery()
            m_userAccountData.UserAccountId = CType(id.Value, Guid)
            m_userAccountData.CreateTimestamp = CType(timestamp.Value, Date)
            m_userAccountData.UpdateTimestamp = CType(timestamp.Value, Date)
        End Using
    End Sub
End Class
