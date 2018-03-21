﻿Public Class TaskDataFactory
    Implements ITaskDataFactory

    Public Property GenericDataFactory As IGenericDataFactory(Of TaskData)

    Public Sub New()
        Me.GenericDataFactory = New GenericDataFactory(Of TaskData)()
    End Sub

    Public Function [Get](settings As ISettings, taskId As Guid) As TaskData Implements ITaskDataFactory.Get
        Return Me.Get(settings, New DbProviderFactory(), taskId)
    End Function

    Public Function [Get](settings As ISettings, ByVal providerFactory As IDbProviderFactory, taskId As Guid) As TaskData
        Dim parameter As IDbDataParameter = CreateParameter(providerFactory, "taskId", DbType.Guid)
        parameter.Value = taskId
        Return Me.GenericDataFactory.GetData(settings,
                                             providerFactory,
                                             "adp.sTask",
                                             Function() New TaskData,
                                             New Action(Of IEnumerable(Of TaskData))(AddressOf AssignDataStateManager(Of TaskData)),
                                             {parameter}).FirstOrDefault
    End Function
End Class
