Public Class EventFormDataSaver
    Implements IDataCreator

    Private m_eventFormData As EventFormData
    Private m_settings As ISettings

    Public Sub New(ByVal settings As ISettings, ByVal eventFormData As EventFormData)
        m_settings = settings
        m_eventFormData = eventFormData
    End Sub

    Public Sub Create() Implements IDataCreator.Create
        Create(New DbProviderFactory)
    End Sub

    Public Sub Create(ByVal providerFactory As IDbProviderFactory)
        Dim timestamp As IDbDataParameter

        If m_eventFormData.DataStateManager.GetState(m_eventFormData) = IDataStateManager(Of UserData).enumState.New Then
            providerFactory.EstablishTransaction(m_settings)
            Using command As IDbCommand = m_settings.Connection.CreateCommand
                command.Transaction = m_settings.Transaction
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "adp.iEventForm"

                timestamp = CreateParameter(providerFactory, "timestamp", DbType.DateTime)
                timestamp.Direction = ParameterDirection.Output
                command.Parameters.Add(timestamp)

                AddParameter(providerFactory, command.Parameters, "eventId", DbType.Guid, GetParameterValue(m_eventFormData.EventId))
                AddParameter(providerFactory, command.Parameters, "formId", DbType.Guid, GetParameterValue(m_eventFormData.FormId))

                command.ExecuteNonQuery()
                m_eventFormData.CreateTimestamp = CType(timestamp.Value, Date)
                m_eventFormData.UpdateTimestamp = CType(timestamp.Value, Date)
            End Using
            m_eventFormData.AcceptChanges()
        End If

    End Sub
End Class
