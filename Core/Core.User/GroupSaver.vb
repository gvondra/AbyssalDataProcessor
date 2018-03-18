Public Class GroupSaver
    Implements IGroupSaver

    Public Sub Save(settings As ISettings, group As IGroup) Implements IGroupSaver.Save
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerSave(settings, group))
    End Sub

    Private Sub InnerSave(settings As ISettings, group As IGroup)
        Dim updater As IDataUpdater
        Dim creator As IDataCreator

        If group.GroupId.Equals(Guid.Empty) Then
            creator = group.GetDataCreator(settings)
            creator.Create()
        Else
            updater = group.GetDataUpdater(settings)
            updater.Update()
        End If
    End Sub
End Class
