Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class MenuController
        Inherits ControllerBase

        <HttpGet(), Authorize()> Public Function GetMenuItems() As IHttpActionResult
            Dim outer As New ArrayList()
            Dim inner As ArrayList

            For Each claim In CType(Me.User, System.Security.Claims.ClaimsPrincipal).Claims
                Debug.WriteLine(claim.ToString)
            Next

            inner = New ArrayList() From {
                New With {.Text = "Home", .URL = ""},
                New With {.Text = "Organizations", .URL = ""}
            }
            outer.Add(inner)

            inner = New ArrayList() From {
                New With {.Text = "Tasks", .URL = ""},
                New With {.Text = "Users", .URL = ""},
                New With {.Text = "Metrics", .URL = ""},
                New With {.Text = "Exceptions", .URL = ""}
            }
            outer.Add(inner)

            Return Ok(outer)
        End Function
    End Class
End Namespace