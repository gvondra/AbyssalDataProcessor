Public Interface ITaskDataFactory
    Function [Get](ByVal settings As ISettings, ByVal taskId As Guid) As TaskData
    Function GetByUserId(ByVal settings As ISettings, ByVal userId As Guid) As IEnumerable(Of TaskData)
End Interface
