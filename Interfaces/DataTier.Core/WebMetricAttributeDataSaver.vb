Public Class WebMetricAttributeDataSaver
    Implements IDataCreator

    Private m_settings As ISettings
    Private m_webMetricAttributeData As WebMetricAttributeData

    Public Sub New(ByVal settings As ISettings, ByVal webMetricAttributeData As WebMetricAttributeData)
        m_settings = settings
        m_webMetricAttributeData = webMetricAttributeData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory())
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Dim id As IDbDataParameter

        providerFactory.EstablishTransaction(m_settings)
        Using command As IDbCommand = m_settings.Connection.CreateCommand
            command.Transaction = m_settings.Transaction
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "adp.iWebMetricAttribute"

            id = CreateParameter(providerFactory, "id", DbType.Int32)
            id.Direction = ParameterDirection.Output
            command.Parameters.Add(id)

            AddParameter(providerFactory, command.Parameters, "WebMetricId", DbType.Int32, GetParameterValue(m_webMetricAttributeData.WebMetricId))
            AddParameter(providerFactory, command.Parameters, "Key", DbType.String, GetParameterValue(m_webMetricAttributeData.Key))
            AddParameter(providerFactory, command.Parameters, "Value", DbType.String, GetParameterValue(m_webMetricAttributeData.Value))

            command.ExecuteNonQuery()
            m_webMetricAttributeData.WebMetricAttributeId = CType(id.Value, Integer)
        End Using
    End Sub
End Class
