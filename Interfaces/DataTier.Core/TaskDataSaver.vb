Public Class TaskDataSaver
    Implements IDataCreator
    Implements IDataUpdater

    Private m_taskData As TaskData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal taskData As TaskData)
        m_settings = settings
        m_taskData = taskData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory)
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Dim id As IDbDataParameter
        Dim timestamp As IDbDataParameter

        If m_taskData.DataStateManager.GetState(m_taskData) = IDataStateManager(Of UserData).enumState.New Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.iTask"

                id = CreateParameter(providerFactory, "id", DbType.Guid)
                id.Direction = ParameterDirection.Output
                command.Parameters.Add(id)

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "taskTypeId", DbType.Guid, GetParameterValue(m_taskData.TaskTypeId))

                command.ExecuteNonQuery()
                m_taskData.TaskId = CType(id.Value, Guid)
                m_taskData.CreateTimestamp = CType(timestamp.Value, Date)
                m_taskData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
            m_taskData.AcceptChanges()
        End If
    End Sub

    Public Sub Update() Implements IDataUpdater.Update
        Update(New DbProviderFactory())
    End Sub

    Public Sub Update(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        If m_taskData.DataStateManager.GetState(m_taskData) = IDataStateManager(Of UserData).enumState.Updated Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.uTask"

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)


                AddParameter(providerFactory, command.Parameters, "id", DbType.Guid, m_taskData.TaskId)

                command.ExecuteNonQuery()
                m_taskData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
            m_taskData.AcceptChanges()
        End If
    End Sub
End Class
