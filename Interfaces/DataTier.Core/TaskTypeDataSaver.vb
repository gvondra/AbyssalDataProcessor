Public Class TaskTypeDataSaver
    Implements IDataCreator
    Implements IDataUpdater

    Private m_taskTypeData As TaskTypeData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal taskTypeData As TaskTypeData)
        m_settings = settings
        m_taskTypeData = taskTypeData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory)
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Dim id As IDbDataParameter
        Dim timestamp As IDbDataParameter

        If m_taskTypeData.DataStateManager.GetState(m_taskTypeData) = IDataStateManager(Of UserData).enumState.New Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.iTaskType"

                id = CreateParameter(providerFactory, "id", DbType.Guid)
                id.Direction = ParameterDirection.Output
                command.Parameters.Add(id)

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "title", DbType.String, GetParameterValue(m_taskTypeData.Title))

                command.ExecuteNonQuery()
                m_taskTypeData.TaskTypeId = CType(id.Value, Guid)
                m_taskTypeData.CreateTimestamp = CType(timestamp.Value, Date)
                m_taskTypeData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
            m_taskTypeData.AcceptChanges()
        End If
    End Sub

    Public Sub Update() Implements IDataUpdater.Update
        Update(New DbProviderFactory)
    End Sub

    Public Sub Update(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        If m_taskTypeData.DataStateManager.GetState(m_taskTypeData) = IDataStateManager(Of UserData).enumState.Updated Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.uTaskType"

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "id", DbType.Guid, GetParameterValue(m_taskTypeData.TaskTypeId))
                AddParameter(providerFactory, command.Parameters, "title", DbType.String, GetParameterValue(m_taskTypeData.Title))

                command.ExecuteNonQuery()
                m_taskTypeData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
            m_taskTypeData.AcceptChanges()
        End If
    End Sub
End Class
