Imports Autofac
Imports AutoMapper
Imports System.IO
Imports System.Net
Imports System.Net.Http
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

        <HttpGet(), ClaimsAuthorization(ClaimTypes:="ANY")> Public Function GetForm(ByVal id As Guid) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim innerForm As IForm = Nothing
            Dim formFactory As IFormFactory
            Dim mapper As IMapper
            Dim mapperConfiguration As FormMapper

            If id.Equals(Guid.Empty) Then
                result = BadRequest("Missing or invalid form id")
            End If

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                formFactory = scope.Resolve(Of IFormFactory)()
                If result Is Nothing Then
                    innerForm = formFactory.Get(New Settings(), id)
                    If innerForm Is Nothing Then
                        result = NotFound()
                    End If
                End If

                If result Is Nothing Then
                    mapperConfiguration = New FormMapper
                    mapper = New Mapper(mapperConfiguration.MapperConfiguration)
                    result = Ok(mapper.Map(Of Form)(innerForm))
                End If
            End Using

            Return result
        End Function

        <HttpGet(), ClaimsAuthorization(ClaimTypes:="ANY"), Route("api/Form/{id}/html")> Public Function GetFormHtml(ByVal id As Guid) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim innerForm As IForm = Nothing
            Dim formFactory As IFormFactory
            Dim formTransformFactory As IFormContentTransormFactory
            Dim formTransform As IFormContentTransform

            If id.Equals(Guid.Empty) Then
                result = BadRequest("Missing or invalid form id")
            End If

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                formFactory = scope.Resolve(Of IFormFactory)()
                If result Is Nothing Then
                    innerForm = formFactory.Get(New Settings(), id)
                    If innerForm Is Nothing Then
                        result = NotFound()
                    End If
                End If

                If result Is Nothing Then
                    formTransformFactory = scope.Resolve(Of IFormContentTransormFactory)()
                    formTransform = formTransformFactory.GetTransform(innerForm)

                    Using stream As Stream = formTransform.Transform()
                        Using reader As StreamReader = New StreamReader(stream)
                            Dim responseMessage As New HttpResponseMessage(HttpStatusCode.OK)
                            responseMessage.Content = New StringContent(reader.ReadToEnd, Encoding.UTF8, "text/html")
                            result = Me.ResponseMessage(responseMessage)
                        End Using
                    End Using
                End If
            End Using

            Return result
        End Function

        'todo add exception handler
        <HttpPost(), Authorize(), Route("api/Form/RoleRequest")> Public Function CreateRoleRequest(<FromBody> ByVal request As RoleRequest) As IHttpActionResult
            Dim userFactory As IUserFactory
            Dim user As IUser
            Dim mapper As IMapper
            Dim innerRequest As Forms.IRoleRequest
            Dim formFactory As IFormFactory
            Dim form As IForm
            Dim eventFactory As IEventFactory
            Dim [event] As IEvent
            Dim triggerFactory As IEventTriggerFactory
            Dim trigger As IEventTrigger
            Dim formSaver As IFormSaver

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                user = userFactory.Get(CType(Me.User, ClaimsPrincipal))

                formFactory = scope.Resolve(Of IFormFactory)()
                innerRequest = formFactory.CreateRoleRequest()
                mapper = New Mapper(m_mapperConfiguration)
                mapper.Map(Of RoleRequest, Forms.IRoleRequest)(request, innerRequest)
                form = innerRequest.CreateForm(user)
                formSaver = scope.Resolve(Of IFormSaver)
                formSaver.Create(New Settings(), form)

                eventFactory = scope.Resolve(Of IEventFactory)()
                [event] = eventFactory.Create(New Settings(), form)
                [event].Message = [event].GetEventType(New Settings).Title & " submitted by " & user.FullName
                If String.Compare(user.FullName, innerRequest.FullName, True) <> 0 Then
                    [event].Message &= " on behalf of " & innerRequest.FullName
                End If
                form = [event].AddForm(form)
                If [event] IsNot Nothing Then
                    triggerFactory = scope.Resolve(Of IEventTriggerFactory)()
                    trigger = triggerFactory.Create
                    trigger.Trigger(New Settings, [event])
                End If
            End Using

            Return Ok()
        End Function
    End Class
End Namespace