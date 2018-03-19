Public Class TaskTypeEventTypeSaver
    Implements ITaskTypeEventTypeSaver

    Public Sub Save(settings As ISettings, types As IEnumerable(Of ITaskTypeEventType)) Implements ITaskTypeEventTypeSaver.Save
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerSave(settings, types))
    End Sub

    Private Sub InnerSave(settings As ISettings, types As IEnumerable(Of ITaskTypeEventType))
        For Each t As ITaskTypeEventType In types
            t.Save(settings)
        Next
    End Sub
End Class
