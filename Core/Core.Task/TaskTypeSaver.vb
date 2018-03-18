Public Class TaskTypeSaver
    Implements ITaskTypeSaver

    Public Sub Save(settings As ISettings, taskType As ITaskType) Implements ITaskTypeSaver.Save
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerSaver(settings, taskType))
    End Sub

    Private Sub InnerSaver(settings As ISettings, taskType As ITaskType)
        Dim creator As IDataCreator
        Dim updater As IDataUpdater

        If taskType.TaskTypeId.Equals(Guid.Empty) Then
            creator = taskType.GetDataCreator(settings)
            creator.Create()
        Else
            updater = taskType.GetDataUpdater(settings)
            updater.Update()
        End If
    End Sub
End Class
