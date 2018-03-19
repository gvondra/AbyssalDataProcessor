Public Class TaskTypeGroupDataSaver
    Implements IDataCreator
    Implements IDataUpdater

    Private m_taskTypeGroupData As TaskTypeGroupData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal taskTypeGroupData As TaskTypeGroupData)
        m_settings = settings
        m_taskTypeGroupData = taskTypeGroupData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory)
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        If m_taskTypeGroupData.DataStateManager.GetState(m_taskTypeGroupData) = IDataStateManager(Of UserData).enumState.New Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.iTaskTypeGroup"

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "taskTypeId", DbType.Guid, GetParameterValue(m_taskTypeGroupData.TaskTypeId))
                AddParameter(providerFactory, command.Parameters, "groupId", DbType.Guid, GetParameterValue(m_taskTypeGroupData.GroupId))
                AddParameter(providerFactory, command.Parameters, "isActive", DbType.Boolean, GetParameterValue(m_taskTypeGroupData.IsActive))

                command.ExecuteNonQuery()
                m_taskTypeGroupData.CreateTimestamp = CType(timestamp.Value, Date)
                m_taskTypeGroupData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
        End If
    End Sub

    Public Sub Update() Implements IDataUpdater.Update
        Update(New DbProviderFactory)
    End Sub

    Public Sub Update(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        If m_taskTypeGroupData.DataStateManager.GetState(m_taskTypeGroupData) = IDataStateManager(Of UserData).enumState.Updated Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.uTaskTypeGroup"

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "taskTypeId", DbType.Guid, GetParameterValue(m_taskTypeGroupData.TaskTypeId))
                AddParameter(providerFactory, command.Parameters, "groupId", DbType.Guid, GetParameterValue(m_taskTypeGroupData.GroupId))
                AddParameter(providerFactory, command.Parameters, "isActive", DbType.Boolean, GetParameterValue(m_taskTypeGroupData.IsActive))

                command.ExecuteNonQuery()
                m_taskTypeGroupData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
        End If
    End Sub
End Class
