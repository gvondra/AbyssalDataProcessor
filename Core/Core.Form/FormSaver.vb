Public Class FormSaver
    Implements IFormSaver

    Public Sub Create(settings As Framework.ISettings, form As IForm) Implements IFormSaver.Create
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerCreate(settings, form))
    End Sub

    Private Sub InnerCreate(settings As ISettings, form As IForm)
        Dim creator As IDataCreator = form.GetDataCreator(settings)
        creator.Create()
    End Sub
End Class
