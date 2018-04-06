Imports System.Security.Claims
Imports System.Net
Imports System.Net.Http
Imports System.Threading
Imports System.Web.Http.Controllers
Imports System.Web.Http
Public Class ClientAuthorizationAttribute
    Inherits AuthorizeAttribute

    Public Overrides Function OnAuthorizationAsync(actionContext As HttpActionContext, cancellationToken As CancellationToken) As Tasks.Task
        Dim principal As ClaimsPrincipal = CType(actionContext.RequestContext.Principal, ClaimsPrincipal)
        Dim organizationId As Guid = Guid.Empty
        Dim ns As String = My.Settings.RoleNameSpace.ToLower
        Dim claim As Claim

        If principal.Identity.IsAuthenticated = False Then
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized)
        Else
            claim = principal.Claims.FirstOrDefault(Function(c As Claim) String.Compare(c.Type, ns & "organizationid", True) = 0)
            If claim Is Nothing Then
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized)
            ElseIf Guid.TryParse(claim.Value, organizationId) = False Then
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized)
            ElseIf organizationId.Equals(Guid.Empty) Then
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized)
            End If
        End If

        Return Tasks.Task.FromResult(Of Object)(Nothing)
    End Function
End Class
