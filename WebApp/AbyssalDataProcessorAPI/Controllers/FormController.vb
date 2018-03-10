Imports Autofac
Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Namespace Controllers
    Public Class FormController
        Inherits ControllerBase

        <HttpPost(), Authorize(), Route("api/Form/RoleRequest")> Public Async Function CreateRoleRequest(<FromBody> ByVal request As RoleRequest) As Threading.Tasks.Task(Of IHttpActionResult)
            Dim userFactory As IUserFactory
            Dim user As IUser
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                user = userFactory.Get(CType(Me.User, ClaimsPrincipal))
            End Using

            Return Ok()
        End Function
    End Class
End Namespace