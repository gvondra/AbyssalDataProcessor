Public Class UserData
    <ColumnMapping("FullName")>
    Public Property UserId As Guid
    <ColumnMapping("FullName")>
    Public Property FullName As String
    <ColumnMapping("ShortName")>
    Public Property ShortName As String
    <ColumnMapping("BirthDate", IsNullable:=True)>
    Public Property BirthDate As Date?
    <ColumnMapping("EmailAddress")>
    Public Property EmailAddress As String
    <ColumnMapping("PhoneNumber")>
    Public Property PhoneNumber As String
    <ColumnMapping("CreateTimestamp")>
    Public Property CreateTimestamp As Date
    <ColumnMapping("UpdateTimestamp")>
    Public Property UpdateTimestamp As Date
End Class
