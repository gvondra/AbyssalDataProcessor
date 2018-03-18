Public Interface ITaskTypeFactory
    Function [Get](ByVal settings As ISettings, ByVal taskTypeId As Guid) As ITaskType
    Function GetAll(ByVal settings As ISettings) As IEnumerable(Of ITaskType)
End Interface
