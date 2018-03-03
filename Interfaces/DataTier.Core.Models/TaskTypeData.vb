Public Class TaskTypeData
    <ColumnMapping("TaskTypeId")>
    Public Property TaskTypeId As Guid
    <ColumnMapping("Title")>
    Public Property Title As String
    <ColumnMapping("CreateTimestamp")>
    Public Property CreateTimestamp As Date
    <ColumnMapping("UpdateTimestamp")>
    Public Property UpdateTimestamp As Date
End Class
