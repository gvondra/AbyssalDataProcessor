Public Class UserGroupSaver
    Implements IUserGroupSaver

    Public Sub Save(settings As ISettings, userGroups As IEnumerable(Of IUserGroup)) Implements IUserGroupSaver.Save
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerSave(settings, userGroups))
    End Sub

    Private Sub InnerSave(settings As ISettings, userGroups As IEnumerable(Of IUserGroup))
        Dim userGroup As IUserGroup

        For Each userGroup In userGroups
            userGroup.Save(settings)
        Next
    End Sub
End Class
