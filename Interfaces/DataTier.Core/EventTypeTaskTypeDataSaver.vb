Public Class EventTypeTaskTypeDataSaver
    Implements IDataCreator
    Implements IDataUpdater

    Private m_eventTypeTaskTypeData As EventTypeTaskTypeData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal eventTypeTaskTypeData As EventTypeTaskTypeData)
        m_settings = settings
        m_eventTypeTaskTypeData = eventTypeTaskTypeData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory)
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        If m_eventTypeTaskTypeData.DataStateManager.GetState(m_eventTypeTaskTypeData) = IDataStateManager(Of UserData).enumState.New Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.iEventTypeTaskType"

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "eventTypeId", DbType.Int16, GetParameterValue(m_eventTypeTaskTypeData.EventTypeId))
                AddParameter(providerFactory, command.Parameters, "taskTypeId", DbType.Guid, GetParameterValue(m_eventTypeTaskTypeData.TaskTypeId))
                AddParameter(providerFactory, command.Parameters, "isActive", DbType.Boolean, GetParameterValue(m_eventTypeTaskTypeData.IsActive))

                command.ExecuteNonQuery()
                m_eventTypeTaskTypeData.CreateTimestamp = CType(timestamp.Value, Date)
                m_eventTypeTaskTypeData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
        End If
    End Sub

    Public Sub Update() Implements IDataUpdater.Update
        Update(New DbProviderFactory)
    End Sub

    Public Sub Update(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        If m_eventTypeTaskTypeData.DataStateManager.GetState(m_eventTypeTaskTypeData) = IDataStateManager(Of UserData).enumState.Updated Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.uEventTypeTaskType"

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "eventTypeId", DbType.Int16, GetParameterValue(m_eventTypeTaskTypeData.EventTypeId))
                AddParameter(providerFactory, command.Parameters, "taskTypeId", DbType.Guid, GetParameterValue(m_eventTypeTaskTypeData.TaskTypeId))
                AddParameter(providerFactory, command.Parameters, "isActive", DbType.Boolean, GetParameterValue(m_eventTypeTaskTypeData.IsActive))

                command.ExecuteNonQuery()
                m_eventTypeTaskTypeData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
        End If
    End Sub
End Class
