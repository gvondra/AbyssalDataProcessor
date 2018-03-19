Public Class TaskTypeGroupSaver
    Implements ITaskTypeGroupSaver

    Public Sub Save(settings As ISettings, types As IEnumerable(Of ITaskTypeGroup)) Implements ITaskTypeGroupSaver.Save
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerSave(settings, types))
    End Sub

    Private Sub InnerSave(settings As ISettings, types As IEnumerable(Of ITaskTypeGroup))
        For Each g As ITaskTypeGroup In types
            g.Save(settings)
        Next
    End Sub
End Class
