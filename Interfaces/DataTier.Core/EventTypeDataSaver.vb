Public Class EventTypeDataSaver
    Implements IDataCreator
    Implements IDataUpdater

    Private m_eventTypeData As EventTypeData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal eventTypeData As EventTypeData)
        m_settings = settings
        m_eventTypeData = eventTypeData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory)
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        providerFactory.EstablishTransaction(m_settings)
        Using command As IDbCommand = m_settings.Connection.CreateCommand
            command.Transaction = m_settings.Transaction
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "adp.iEventType"

            timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
            timestamp.Direction = ParameterDirection.Output
            command.Parameters.Add(timestamp)

            AddParameter(providerFactory, command.Parameters, "id", DbType.Int16, GetParameterValue(m_eventTypeData.EventTypeId))
            AddParameter(providerFactory, command.Parameters, "title", DbType.String, GetParameterValue(m_eventTypeData.Title))

            command.ExecuteNonQuery()
            m_eventTypeData.CreateTimestamp = CType(timestamp.Value, Date)
            m_eventTypeData.UpdateTimestamp = CType(timestamp.Value, Date)
        End Using
    End Sub

    Public Sub Update() Implements IDataUpdater.Update
        Update(New DbProviderFactory)
    End Sub

    Public Sub Update(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        providerFactory.EstablishTransaction(m_settings)
        Using command As IDbCommand = m_settings.Connection.CreateCommand
            command.Transaction = m_settings.Transaction
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "adp.uEventType"

            timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
            timestamp.Direction = ParameterDirection.Output
            command.Parameters.Add(timestamp)

            AddParameter(providerFactory, command.Parameters, "id", DbType.Int16, GetParameterValue(m_eventTypeData.EventTypeId))
            AddParameter(providerFactory, command.Parameters, "title", DbType.String, GetParameterValue(m_eventTypeData.Title))

            command.ExecuteNonQuery()
            m_eventTypeData.UpdateTimestamp = CType(timestamp.Value, Date)
        End Using
    End Sub
End Class
