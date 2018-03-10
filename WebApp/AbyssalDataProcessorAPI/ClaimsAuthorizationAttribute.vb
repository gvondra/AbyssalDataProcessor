﻿Imports System.Security.Claims
Imports System.Net
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Web.Http.Controllers
Imports System.Web.Http
Public Class ClaimsAuthorizationAttribute
    Inherits AuthorizeAttribute

    Private Property CLAIM_NAMESPACE As String = "https://abyssaldataprocessor-dvlp/role-"

    Public Property ClaimTypes As String

    Public Overrides Function OnAuthorizationAsync(actionContext As HttpActionContext, cancellationToken As CancellationToken) As Task
        Dim principal As ClaimsPrincipal = CType(actionContext.RequestContext.Principal, ClaimsPrincipal)

        If principal.Identity.IsAuthenticated = False Then
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized)
        ElseIf String.IsNullOrEmpty(ClaimTypes) = False Then
            If principal.Claims.Select(Of String)(Function(c As Claim) c.Type.ToLower()) _
                    .Intersect(ClaimTypes.Split("|"c, ":"c).Select(Of String)(Function(c As String) CLAIM_NAMESPACE & c.ToLower())) _
                    .Any() = False Then

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized)
            End If
        End If

        Return Task.FromResult(Of Object)(Nothing)
    End Function
End Class

