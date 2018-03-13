Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class FormController
        Inherits ControllerBase

        Private Shared m_mapperConfiguration As MapperConfiguration

        Shared Sub New()
            m_mapperConfiguration = New MapperConfiguration(Sub(c As IMapperConfigurationExpression)
                                                                c.CreateMap(Of RoleRequest, Forms.IRoleRequest)()
                                                            End Sub)
        End Sub

        'todo add exception handler
        'todo add role request event trigger
        <HttpPost(), Authorize(), Route("api/Form/RoleRequest")> Public Function CreateRoleRequest(<FromBody> ByVal request As RoleRequest) As IHttpActionResult
            Dim userFactory As IUserFactory
            Dim user As IUser
            Dim mapper As IMapper
            Dim innerRequest As Forms.IRoleRequest
            Dim formFactory As IFormFactory
            Dim saver As IFormSaver

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                user = userFactory.Get(CType(Me.User, ClaimsPrincipal))

                formFactory = scope.Resolve(Of IFormFactory)()
                innerRequest = formFactory.CreateRoleRequest()
                mapper = New Mapper(m_mapperConfiguration)
                mapper.Map(Of RoleRequest, Forms.IRoleRequest)(request, innerRequest)
                saver = scope.Resolve(Of IFormSaver)()
                saver.Create(New Settings(), innerRequest.CreateForm(user))
            End Using

            Return Ok()
        End Function
    End Class
End Namespace