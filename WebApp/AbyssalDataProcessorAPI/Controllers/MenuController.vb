Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class MenuController
        Inherits ControllerBase

        <HttpGet(), Authorize()> Public Function GetMenuItems() As IHttpActionResult
            Dim column1 As New ArrayList
            Dim column2 As New ArrayList
            Dim column3 As New ArrayList
            Dim result As New ArrayList
            Dim flags As enumRole = RoleProcessor.GetRoleFlags(CType(Me.User, ClaimsPrincipal))

            column1.Add(New With {.Text = "Home", .URL = ""})

            If (flags And enumRole.OrganizationAdminstrator) = enumRole.OrganizationAdminstrator Then
                column1.Add(New With {.Text = "Organizations", .URL = ""})
            End If

            If flags = enumRole.None Then
                column1.Add(New With {.Text = "Role Request", .URL = "rolerequest"})
            End If

            If (flags And enumRole.TaskAdministrator) = enumRole.TaskAdministrator Then
                column2.Add(New With {.Text = "Tasks", .URL = ""})
            End If

            If (flags And enumRole.UserAdministrator) = enumRole.UserAdministrator Then
                column2.Add(New With {.Text = "Users", .URL = ""})
            End If

            If flags <> enumRole.None Then
                column3.Add(New With {.Text = "Metrics", .URL = ""})
                column3.Add(New With {.Text = "Exceptions", .URL = ""})
            End If

            If column1.Count > 0 Then
                result.Add(column1)
            End If
            If column2.Count > 0 Then
                result.Add(column2)
            End If
            If column3.Count > 0 Then
                result.Add(column3)
            End If

            Return Ok(result)
        End Function
    End Class
End Namespace