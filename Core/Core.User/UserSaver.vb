Public Class UserSaver
    Implements IUserSaver

    Public Sub Save(settings As ISettings, user As IUser) Implements IUserSaver.Save
        Save(settings, user, Nothing)
    End Sub

    Public Sub Save(settings As ISettings, user As IUser, subscriber As String) Implements IUserSaver.Save
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerSave(settings, user, subscriber))
    End Sub

    Private Sub InnerSave(settings As ISettings, user As IUser, subscriber As String)
        Dim creator As IDataCreator
        Dim updater As IDataUpdater

        If user.UserId.Equals(Guid.Empty) Then
            creator = user.GetDataCreator(settings)
            creator.Create()
        Else
            updater = user.GetDataUpdater(settings)
            updater.Update()
        End If

        If String.IsNullOrEmpty(subscriber) = False Then
            creator = user.GetAccountDataCreater(settings, subscriber)
            creator.Create()
        End If
    End Sub
End Class
