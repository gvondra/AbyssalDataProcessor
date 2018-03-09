﻿Public Class UserDataSaver
    Implements IDataCreator
    Implements IDataUpdater

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
        Dim id As IDbDataParameter
        Dim timestamp As IDbDataParameter

        providerFactory.EstablishTransaction(m_settings)
        Using command As IDbCommand = m_settings.Connection.CreateCommand
            command.Transaction = m_settings.Transaction
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "adp.iUser"

            id = CreateParameter(providerFactory, "id", DbType.Guid)
            id.Direction = ParameterDirection.Output
            command.Parameters.Add(id)

            timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
            timestamp.Direction = ParameterDirection.Output
            command.Parameters.Add(timestamp)

            CommonParameters(providerFactory, command)

            command.ExecuteNonQuery()
            m_userData.UserId = CType(id.Value, Guid)
            m_userData.CreateTimestamp = CType(timestamp.Value, Date)
            m_userData.UpdateTimestamp = CType(timestamp.Value, Date)
        End Using
    End Sub

    Public Sub Update() Implements IDataUpdater.Update
        Update(New DbProviderFactory())
    End Sub

    Public Sub Update(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        providerFactory.EstablishTransaction(m_settings)
        Using command As IDbCommand = m_settings.Connection.CreateCommand
            command.Transaction = m_settings.Transaction
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "adp.uUser"

            AddParameter(providerFactory, command.Parameters, "id", DbType.Guid, m_userData.UserId)

            timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
            timestamp.Direction = ParameterDirection.Output
            command.Parameters.Add(timestamp)

            CommonParameters(providerFactory, command)

            command.ExecuteNonQuery()
            m_userData.UpdateTimestamp = CType(timestamp.Value, Date)
        End Using
    End Sub

    Private Sub CommonParameters(ByVal providerFactory As IDbProviderFactory, ByVal command As IDbCommand)
        AddParameter(providerFactory, command.Parameters, "fullName", DbType.String, GetParameterValue(m_userData.FullName))
        AddParameter(providerFactory, command.Parameters, "shortName", DbType.String, GetParameterValue(m_userData.ShortName))
        AddParameter(providerFactory, command.Parameters, "birthDate", DbType.Date, GetParameterValue(m_userData.BirthDate))
        AddParameter(providerFactory, command.Parameters, "emailAddress", DbType.String, GetParameterValue(m_userData.EmailAddress))
        AddParameter(providerFactory, command.Parameters, "phoneNumber", DbType.AnsiString, GetParameterValue(m_userData.PhoneNumber))
    End Sub
End Class
