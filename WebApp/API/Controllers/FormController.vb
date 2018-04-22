Imports System.Net
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class FormController
        Inherits ControllerBase

        'todo add exception handler
        '<HttpPost(), Authorize(), Route("api/Form/RoleRequest")> Public Function CreateRoleRequest(<FromBody> ByVal request As RoleRequest) As IHttpActionResult
        'Dim userFactory As IUserFactory
        'Dim user As IUser
        'Dim mapper As IMapper
        'Dim innerRequest As Forms.IRoleRequest
        'Dim formFactory As IFormFactory
        'Dim form As IForm
        'Dim eventFactory As IEventFactory
        'Dim [event] As IEvent
        'Dim triggerFactory As IEventTriggerFactory
        'Dim trigger As IEventTrigger
        'Dim formSaver As IFormSaver

        'Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
        '    userFactory = scope.Resolve(Of IUserFactory)()
        '    user = userFactory.Get(CType(Me.User, ClaimsPrincipal))

        '    formFactory = scope.Resolve(Of IFormFactory)()
        '    innerRequest = formFactory.CreateRoleRequest()
        '    mapper = New Mapper(m_mapperConfiguration)
        '    mapper.Map(Of RoleRequest, Forms.IRoleRequest)(request, innerRequest)
        '    form = innerRequest.CreateForm(user)
        '    formSaver = scope.Resolve(Of IFormSaver)
        '    formSaver.Create(New Settings(), form)

        '    eventFactory = scope.Resolve(Of IEventFactory)()
        '    [event] = eventFactory.Create(New Settings(), form)
        '    [event].Message = [event].GetEventType(New Settings).Title & " submitted by " & user.FullName
        '    If String.Compare(user.FullName, innerRequest.FullName, True) <> 0 Then
        '        [event].Message &= " on behalf of " & innerRequest.FullName
        '    End If
        '    form = [event].AddForm(form)
        '    If [event] IsNot Nothing Then
        '        triggerFactory = scope.Resolve(Of IEventTriggerFactory)()
        '        trigger = triggerFactory.Create
        '        trigger.Trigger(New Settings, [event])
        '    End If
        'End Using

        'Return Ok()
        'End Function
    End Class
End Namespace