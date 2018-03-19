Public Class EventTypeTaskTypeDataFactory
    Implements IEventTypeTaskTypeDataFactory

    Public Property GenericDataFactory As IGenericDataFactory(Of EventTypeTaskTypeData)

    Public Sub New()
        Me.GenericDataFactory = New GenericDataFactory(Of EventTypeTaskTypeData)()
    End Sub

    Public Function GetByTaskId(settings As ISettings, taskTypeId As Guid) As IEnumerable(Of EventTypeTaskTypeData) Implements IEventTypeTaskTypeDataFactory.GetByTaskId
        Return Me.GetByTaskId(settings, New DbProviderFactory(), taskTypeId)
    End Function

    Public Function GetByTaskId(settings As ISettings, ByVal providerFactory As IDbProviderFactory, taskTypeId As Guid) As IEnumerable(Of EventTypeTaskTypeData)
        Dim parameter As IDbDataParameter
        Dim reader As IDataReader
        Dim data As EventTypeTaskTypeData
        Dim result As List(Of EventTypeTaskTypeData)
        Dim eventTypes As IEnumerable(Of EventTypeData)
        Using connection As IDbConnection = providerFactory.OpenConnection(settings.ConnectionString)
            Using command As IDbCommand = connection.CreateCommand
                command.CommandText = "adp.sEventTypeTaskTypeByTaskTypeId"
                command.CommandType = CommandType.StoredProcedure
                parameter = CreateParameter(providerFactory, "taskTypeId", DbType.Guid)
                parameter.Value = taskTypeId
                command.Parameters.Add(parameter)

                reader = command.ExecuteReader()
                result = New List(Of EventTypeTaskTypeData)(Me.GenericDataFactory.LoadData(Of EventTypeTaskTypeData)(
                    reader, Function() New EventTypeTaskTypeData,
                    New Action(Of IEnumerable(Of EventTypeTaskTypeData))(AddressOf AssignDataStateManager(Of EventTypeTaskTypeData))
                ))

                If reader.NextResult Then
                    eventTypes = Me.GenericDataFactory.LoadData(Of EventTypeData)(
                        reader,
                        Function() New EventTypeData,
                        New Action(Of IEnumerable(Of EventTypeData))(AddressOf AssignDataStateManager(Of EventTypeData))
                    )
                    For Each data In result
                        data.EventType = eventTypes.FirstOrDefault(Function(et As EventTypeData) et.EventTypeId.Equals(data.EventTypeId))
                    Next data
                End If
                reader.Close()
            End Using
        End Using
        Return result
    End Function
End Class
