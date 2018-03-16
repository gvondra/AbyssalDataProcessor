Public Class EventTypeTaskTypeData
    <ColumnMapping("EventTypeId")> Public Property EventTypeId As Short
    <ColumnMapping("TaskTypeId")> Public Property TaskTypeId As Guid
    <ColumnMapping("IsActive")> Public Property IsActive As Boolean
    <ColumnMapping("CreateTimestamp")> Public Property CreateTimestamp As Date
    <ColumnMapping("UpdateTimestamp")> Public Property UpdateTimestamp As Date
End Class
