Public Class EventTriggerFactory
    Implements IEventTriggerFactory

    Public Function Create() As IEventTriggerAggregator Implements IEventTriggerFactory.Create
        Dim aggregator As New EventTriggerAggregator()

        aggregator.Add(New TaskEventTrigger)

        Return aggregator
    End Function
End Class
