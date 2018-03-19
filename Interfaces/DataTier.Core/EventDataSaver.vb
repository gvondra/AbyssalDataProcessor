Public Class EventDataSaver
    Implements IDataCreator

    Private m_eventData As EventData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal eventData As EventData)
        m_settings = settings
        m_eventData = eventData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory)
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Dim id As IDbDataParameter
        Dim timestamp As IDbDataParameter

        If m_eventData.DataStateManager.GetState(m_eventData) = IDataStateManager(Of UserData).enumState.New Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.iEvent"

                id = CreateParameter(providerFactory, "id", DbType.Guid)
                id.Direction = ParameterDirection.Output
                command.Parameters.Add(id)

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "eventTypeId", DbType.Int16, GetParameterValue(m_eventData.EventTypeId))

                command.ExecuteNonQuery()
                m_eventData.EventId = CType(id.Value, Guid)
                m_eventData.CreateTimestamp = CType(timestamp.Value, Date)
                m_eventData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
            m_eventData.AcceptChanges()
        End If
    End Sub
End Class
