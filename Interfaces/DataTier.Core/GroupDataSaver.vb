﻿Public Class GroupDataSaver
    Implements IDataCreator
    Implements IDataUpdater

    Private m_groupData As GroupData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal groupData As GroupData)
        m_settings = settings
        m_groupData = groupData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory)
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Dim id As IDbDataParameter
        Dim timestamp As IDbDataParameter

        If m_groupData.DataStateManager.GetState(m_groupData) = IDataStateManager(Of UserData).enumState.New Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.iGroup"

                id = CreateParameter(providerFactory, "id", DbType.Guid)
                id.Direction = ParameterDirection.Output
                command.Parameters.Add(id)

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "name", DbType.String, GetParameterValue(m_groupData.Name))

                command.ExecuteNonQuery()
                m_groupData.GroupId = CType(id.Value, Guid)
                m_groupData.CreateTimestamp = CType(timestamp.Value, Date)
                m_groupData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
            m_groupData.AcceptChanges()
        End If
    End Sub

    Public Sub Update() Implements IDataUpdater.Update
        Update(New DbProviderFactory)
    End Sub

    Public Sub Update(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        If m_groupData.DataStateManager.GetState(m_groupData) = IDataStateManager(Of UserData).enumState.Updated Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.uGroup"

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "id", DbType.Guid, GetParameterValue(m_groupData.GroupId))
                AddParameter(providerFactory, command.Parameters, "name", DbType.String, GetParameterValue(m_groupData.Name))

                command.ExecuteNonQuery()
                m_groupData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
            m_groupData.AcceptChanges()
        End If
    End Sub
End Class
