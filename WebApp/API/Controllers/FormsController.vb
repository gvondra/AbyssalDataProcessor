Imports Autofac
Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Namespace Controllers
    Public Class FormsController
        Inherits ControllerBase

        <HttpPost(), ClientAuthorization()> Public Function CreateForm(<FromBody> ByVal request As CreateFormRequest) As IHttpActionResult
            Dim userFactory As IUserFactory
            Dim user As IUser
            Dim formFactory As IFormFactory
            Dim form As IForm
            Dim organizationId As Guid
            Dim response As IHttpActionResult = Nothing
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                organizationId = GetOrganizationId()

                If request.UserId.Equals(Guid.Empty) Then
                    response = BadRequest("Missing or Invalid User Id")
                End If

                If response Is Nothing Then
                    userFactory = scope.Resolve(Of IUserFactory)()
                    user = userFactory.Get(New Settings(), GetOrganizationId, request.UserId)
                    If user Is Nothing Then
                        response = BadRequest("User not found")
                    End If
                End If

                If response Is Nothing Then
                    formFactory = scope.Resolve(Of IFormFactory)()
                End If
            End Using
            Return response
        End Function
    End Class
End Namespace