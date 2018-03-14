Public Class EventSaver
    Implements IEventSaver

    Public Sub Create(settings As ISettings, [event] As IEvent) Implements IEventSaver.Create
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerCreate(settings, [event]))
    End Sub

    Private Sub InnerCreate(settings As ISettings, [event] As IEvent)
        Dim creator As IDataCreator = [event].GetDataCreator(settings)
        creator.Create()
    End Sub
End Class
