Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Namespace Controllers
    Public Class UserController
        Inherits ApiController

        <HttpGet(), Authorize()> Public Function GetUser() As IHttpActionResult
            Dim user As ClaimsPrincipal = CType(Me.User, ClaimsPrincipal)
            Dim claim As Claim = user.Claims.FirstOrDefault(Function(c As Claim) c.Type = ClaimTypes.NameIdentifier)

            If claim IsNot Nothing Then

            End If

            Return Ok()
        End Function

    End Class
End Namespace