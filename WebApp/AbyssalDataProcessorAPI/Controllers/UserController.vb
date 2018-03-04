Imports Autofac
Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Namespace Controllers
    Public Class UserController
        Inherits ApiController

        Private m_container As IContainer

        Public Sub New()
            m_container = ObjectContainer.GetContainer()
        End Sub

        Public Sub New(ByVal container As IContainer)
            m_container = container
        End Sub

        <HttpGet(), Authorize()> Public Function GetUser() As IHttpActionResult
            Dim user As ClaimsPrincipal = CType(Me.User, ClaimsPrincipal)
            Dim claim As Claim = user.Claims.FirstOrDefault(Function(c As Claim) c.Type = ClaimTypes.NameIdentifier)
            Dim userFactory As IUserFactory
            Dim u As IUser
            Using scope As ILifetimeScope = m_container.BeginLifetimeScope
                If claim IsNot Nothing Then
                    userFactory = scope.Resolve(Of IUserFactory)()
                    u = userFactory.GetBySubscriberId(New Settings(), claim.Value)
                End If
            End Using


            Return Ok()
        End Function

    End Class
End Namespace