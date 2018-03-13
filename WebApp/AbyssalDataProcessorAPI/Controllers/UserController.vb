Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class UserController
        Inherits ControllerBase

        Private Shared m_mapperConfiguration As MapperConfiguration

        Shared Sub New()
            m_mapperConfiguration = New MapperConfiguration(Sub(c As IMapperConfigurationExpression)
                                                                c.CreateMap(Of IUser, User)()
                                                            End Sub)
        End Sub

        'todo add error handling
        <HttpGet(), Authorize()> Public Shadows Function GetUser() As IHttpActionResult
            Dim userFactory As IUserFactory
            Dim u As IUser = Nothing
            Dim mapper As IMapper
            Dim result As IHttpActionResult = Nothing

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                u = userFactory.Get(CType(Me.User, ClaimsPrincipal))
            End Using

            If u Is Nothing Then
                result = NotFound()
            End If

            If result Is Nothing Then
                mapper = New Mapper(m_mapperConfiguration)
                result = Ok(mapper.Map(Of User)(u))
            End If

            Return result
        End Function

    End Class
End Namespace