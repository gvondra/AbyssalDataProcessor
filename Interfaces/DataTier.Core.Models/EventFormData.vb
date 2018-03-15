Public Class EventFormData
    <ColumnMapping("EventId")> Public Property EventId As Guid
    <ColumnMapping("FormId")> Public Property FormId As Guid
    <ColumnMapping("CreateTimestamp")> Public Property CreateTimestamp As Date
    <ColumnMapping("UpdateTimestamp")> Public Property UpdateTimestamp As Date
End Class
