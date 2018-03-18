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

    Public Sub Update(settings As ISettings, eventType As IEventType) Implements IEventTypeSaver.Update
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerUpdate(settings, eventType))
    End Sub

    Public Sub InnerUpdate(settings As ISettings, eventType As IEventType)
        Dim updater As IDataUpdater = eventType.GetDataUpdater(settings)
        updater.Update()
    End Sub

End Class
