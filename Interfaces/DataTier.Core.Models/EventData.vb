Public Class EventData
    <ColumnMapping("EventId")> Public Property EventId As Guid
    <ColumnMapping("EventTypeId")> Public Property EventTypeId As Short
    <ColumnMapping("CreateTimestamp")> Public Property CreateTimestamp As Date
    <ColumnMapping("UpdateTimestamp")> Public Property UpdateTimestamp As Date
End Class
