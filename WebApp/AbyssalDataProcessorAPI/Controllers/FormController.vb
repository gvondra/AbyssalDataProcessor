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
            Dim form As IForm
            Dim eventFactory As IEventFactory
            Dim eventSaver As IEventSaver
            Dim [event] As IEvent
            Dim createEventRequest As IEventSaver.ICreateEventRequest

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                user = userFactory.Get(CType(Me.User, ClaimsPrincipal))

                formFactory = scope.Resolve(Of IFormFactory)()
                innerRequest = formFactory.CreateRoleRequest()
                mapper = New Mapper(m_mapperConfiguration)
                mapper.Map(Of RoleRequest, Forms.IRoleRequest)(request, innerRequest)
                saver = scope.Resolve(Of IFormSaver)()
                form = innerRequest.CreateForm(user)
                saver.Create(New Settings(), form)

                eventFactory = scope.Resolve(Of IEventFactory)()
                [event] = eventFactory.Create(New Settings(), form)
                form = [event].AddForm(form)
                If [event] IsNot Nothing Then
                    createEventRequest = scope.Resolve(Of IEventSaver.ICreateEventRequest)(New TypedParameter(GetType(IEvent), [event]))
                    createEventRequest.Forms = {form}
                    eventSaver = scope.Resolve(Of IEventSaver)()
                    eventSaver.Create(New Settings(), createEventRequest)
                End If
            End Using

            Return Ok()
        End Function
    End Class
End Namespace