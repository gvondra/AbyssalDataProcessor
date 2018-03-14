Public Class EventTypeSaver
    Implements IEventTypeSaver

    Public Sub Create(settings As ISettings, eventType As IEventType) Implements IEventTypeSaver.Create
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerCreate(settings, eventType))
    End Sub

    Private Sub InnerCreate(settings As ISettings, eventType As IEventType)
        Dim creator As IDataCreator = eventType.GetDataCreator(settings)
        creator.Create()
    End Sub
End Class
