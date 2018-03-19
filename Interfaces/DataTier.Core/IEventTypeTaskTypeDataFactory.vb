Public Interface IEventTypeTaskTypeDataFactory
    Function GetByTaskId(ByVal settings As ISettings, ByVal taskId As Guid) As IEnumerable(Of EventTypeTaskTypeData)
End Interface
