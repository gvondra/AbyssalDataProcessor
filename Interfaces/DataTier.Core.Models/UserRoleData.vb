Public Class UserRoleData
    <ColumnMapping("UserId")>
    Public Property UserId As Guid
    <ColumnMapping("RoleTypeId")>
    Public Property RoleTypeId As Guid
    <ColumnMapping("IsActive")>
    Public Property IsActive As Boolean
    <ColumnMapping("CreateTimestamp")>
    Public Property CreateTimestamp As Date
    <ColumnMapping("UpdateTimestamp")>
    Public Property UpdateTimestamp As Date
End Class
