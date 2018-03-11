Imports System.Xml
Public Class FormData
    <ColumnMapping("FormId")> Public Property FormId As Guid
    <ColumnMapping("UserId")> Public Property UserId As Guid
    <ColumnMapping("FormTypeId")> Public Property FormTypeId As Short
    <ColumnMapping("Style")> Public Property Style As Short
    <ColumnMapping("Content", IsNullable:=True)> Public Property Content As XmlNode
    <ColumnMapping("CreateTimestamp")> Public Property CreateTimestamp As Date
    <ColumnMapping("UpdateTimestamp")> Public Property UpdateTimestamp As Date
End Class
