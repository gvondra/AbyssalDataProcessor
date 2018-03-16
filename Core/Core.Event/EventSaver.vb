Public Class EventSaver
    Implements IEventSaver

    Public Sub Create(settings As ISettings, request As IEventSaver.ICreateEventRequest) Implements IEventSaver.Create
        Dim saver As New Saver()
        saver.Save(settings, Sub() InnerCreate(settings, request))
    End Sub

    Private Sub InnerCreate(settings As ISettings, request As IEventSaver.ICreateEventRequest)
        Dim creator As IDataCreator = request.[Event].GetDataCreator(settings)
        creator.Create()

        If request.Forms IsNot Nothing Then
            For Each form As IForm In request.Forms
                creator = form.GetDataCreator(settings)
                creator.Create()
            Next
        End If
    End Sub

    Public Class CreateEventRequest
        Implements IEventSaver.ICreateEventRequest

        Public Property [Event] As IEvent Implements IEventSaver.ICreateEventRequest.Event
        Public Property Forms As IEnumerable(Of IForm) Implements IEventSaver.ICreateEventRequest.Forms

        Public Sub New([event] As IEvent)
            Me.Event = [event]
        End Sub
    End Class
End Class
