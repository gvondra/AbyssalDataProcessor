Imports Autofac
Imports System.Security.Claims
Imports System.Web.Http
Public MustInherit Class ControllerBase
    Inherits ApiController

    Private m_container As IContainer

    Public Sub New()
        m_container = AbyssalDataProcessorAPI.ObjectContainer.GetContainer()
    End Sub

    Public Function GetOrganizationId() As Guid
        Dim ns As String = My.Settings.RoleNameSpace.ToLower
        Dim principal As ClaimsPrincipal = CType(Me.User, ClaimsPrincipal)
        Return Guid.Parse(
            principal.Claims.First(Function(c As Claim) String.Compare(c.Type, ns & "organizationid", True) = 0).Value
        )
    End Function

    Public Property ObjectContainer As IContainer
        Get
            Return m_container
        End Get
        Set(value As IContainer)
            m_container = value
        End Set
    End Property
End Class
