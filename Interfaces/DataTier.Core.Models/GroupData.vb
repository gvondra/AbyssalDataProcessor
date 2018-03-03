Public Class GroupData
    <ColumnMapping("GroupId")>
    Public Property GroupId As Guid
    <ColumnMapping("Name")>
    Public Property Name As String
    <ColumnMapping("CreateTimestamp")>
    Public Property CreateTimestamp As Date
    <ColumnMapping("UpdateTimestamp")>
    Public Property UpdateTimestamp As Date
End Class
