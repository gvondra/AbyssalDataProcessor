Public Class WebMetricDataSaver
    Implements IDataCreator

    Private m_settings As ISettings
    Private m_webMetricData As WebMetricData

    Public Sub New(ByVal settings As ISettings, ByVal webMetricData As WebMetricData)
        m_settings = settings
        m_webMetricData = webMetricData
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
            command.CommandText = "adp.iWebMetric"

            id = CreateParameter(providerFactory, "id", DbType.Int32)
            id.Direction = ParameterDirection.Output
            command.Parameters.Add(id)

            AddParameter(providerFactory, command.Parameters, "Url", DbType.String, GetParameterValue(m_webMetricData.Url))
            AddParameter(providerFactory, command.Parameters, "Method", DbType.String, GetParameterValue(m_webMetricData.Method))
            AddParameter(providerFactory, command.Parameters, "CreateTimestamp", DbType.DateTime, GetParameterValue(m_webMetricData.CreateTimestamp))
            AddParameter(providerFactory, command.Parameters, "Duration", DbType.Double, GetParameterValue(m_webMetricData.Duration))
            AddParameter(providerFactory, command.Parameters, "Status", DbType.String, GetParameterValue(m_webMetricData.Status))
            AddParameter(providerFactory, command.Parameters, "Controller", DbType.String, GetParameterValue(m_webMetricData.Controller))

            command.ExecuteNonQuery()
            m_webMetricData.WebMetricId = CType(id.Value, Integer)
        End Using
    End Sub
End Class
