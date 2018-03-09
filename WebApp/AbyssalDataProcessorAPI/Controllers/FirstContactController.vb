Imports Autofac
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class FirstContactController
        Inherits ControllerBase

        <HttpPost(), Authorize()> Public Function FirstContact(ByVal code As String) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim user As IUser = Nothing

            If String.IsNullOrEmpty(ConfigurationManager.AppSettings("FirstContactCode")) OrElse code <> ConfigurationManager.AppSettings("FirstContactCode") Then
                result = Unauthorized()
            End If

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                user = Me.GetUserObject
            End Using

            Return result
        End Function
    End Class
End Namespace