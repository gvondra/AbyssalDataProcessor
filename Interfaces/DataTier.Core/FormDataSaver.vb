Imports System.Xml
Public Class FormDataSaver
    Implements IDataCreator

    Private m_formData As FormData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal formData As FormData)
        m_settings = settings
        m_formData = formData
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
            command.CommandText = "adp.iForm"

            id = CreateParameter(providerFactory, "id", DbType.Guid)
            id.Direction = ParameterDirection.Output
            command.Parameters.Add(id)

            timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
            timestamp.Direction = ParameterDirection.Output
            command.Parameters.Add(timestamp)

            AddParameter(providerFactory, command.Parameters, "userId", DbType.String, m_formData.UserId)
            AddParameter(providerFactory, command.Parameters, "formTypeId", DbType.Int16, m_formData.FormTypeId)
            AddParameter(providerFactory, command.Parameters, "style", DbType.Int16, m_formData.Style)
            AddParameter(providerFactory, command.Parameters, "content", DbType.Xml, m_formData.Content)

            command.ExecuteNonQuery()
            m_formData.FormId = CType(id.Value, Guid)
            m_formData.CreateTimestamp = CType(timestamp.Value, Date)
            m_formData.UpdateTimestamp = CType(timestamp.Value, Date)
        End Using
    End Sub
End Class
