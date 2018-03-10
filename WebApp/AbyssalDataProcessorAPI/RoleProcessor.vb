Imports AbyssalDataProcessor.Core.User
Imports System.Security.Claims
Public NotInheritable Class RoleProcessor
    Private Sub New()
    End Sub

    Public Shared Function GetRoleFlags(ByVal principal As ClaimsPrincipal) As enumRole
        Dim role As enumRole = enumRole.None
        Dim ns As String = My.Settings.RoleNameSpace.ToLower & "role-"
        Dim roleCodes As New List(Of String)() From {
            RoleConstants.ORGANIZATION_ADMINISTRATOR,
            RoleConstants.TASK_ADMINISTRATOR,
            RoleConstants.SUPER_USER,
            RoleConstants.USER_ADMINISTRATOR
        }

        If principal.Claims.Where(Function(c As Claim) c.Type = "gty" And c.Value = "client-credentials").Any() Then
            role = role Or enumRole.SuperUser
        End If

        For Each claim As Claim In principal.Claims.Where(Function(c As Claim) roleCodes.Any(Function(rc As String) String.Compare(ns & rc, c.Type, True) = 0))
            If String.Compare(claim.Value, RoleConstants.ORGANIZATION_ADMINISTRATOR, True) = 0 Then
                role = role Or enumRole.OrganizationAdminstrator
            End If
            If String.Compare(claim.Value, RoleConstants.TASK_ADMINISTRATOR, True) = 0 Then
                role = role Or enumRole.TaskAdministrator
            End If
            If String.Compare(claim.Value, RoleConstants.USER_ADMINISTRATOR, True) = 0 Then
                role = role Or enumRole.UserAdministrator
            End If
            If String.Compare(claim.Value, RoleConstants.SUPER_USER, True) = 0 Then
                role = role Or enumRole.SuperUser
            End If
        Next

        Return role
    End Function
End Class
