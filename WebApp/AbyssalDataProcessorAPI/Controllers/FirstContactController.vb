Imports Autofac
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class FirstContactController
        Inherits ControllerBase

        <HttpPost(), Authorize()> Public Function FirstContact(<FromBody> ByVal code As String) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim user As IUser = Nothing
            Dim userFactory As IUserFactory

            If String.IsNullOrEmpty(ConfigurationManager.AppSettings("FirstContactCode")) OrElse code <> ConfigurationManager.AppSettings("FirstContactCode") Then
                result = StatusCode(HttpStatusCode.Forbidden)
            End If

            If result Is Nothing Then
                user = Me.GetUserObject
                Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                    userFactory = scope.Resolve(Of IUserFactory)()
                    If userFactory.GetAccountCount(New Settings()) <> 1 Then
                        result = StatusCode(HttpStatusCode.Forbidden)
                    End If
                End Using
            End If

            If result Is Nothing Then

            End If

            Return result
        End Function
    End Class
End Namespace