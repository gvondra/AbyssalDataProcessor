Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class TaskTypeController
        Inherits ControllerBase

        Private Shared m_mapperConfiguration As MapperConfiguration

        Shared Sub New()
            m_mapperConfiguration = New MapperConfiguration(Sub(exp As IMapperConfigurationExpression)
                                                                exp.CreateMap(Of ITaskType, TaskType)()
                                                                exp.CreateMap(Of TaskType, ITaskType)()
                                                            End Sub)
        End Sub

        'todo add error handling
        <HttpGet(), ClaimsAuthorization(ClaimTypes:="TA")> Public Shadows Function GetTaskType(ByVal id As Guid) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim innerTaskType As ITaskType
            Dim taskTypeFactory As ITaskTypeFactory
            Dim mapper As IMapper
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                taskTypeFactory = scope.Resolve(Of ITaskTypeFactory)()
                innerTaskType = taskTypeFactory.Get(New Settings(), id)

                If innerTaskType Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    mapper = New Mapper(m_mapperConfiguration)
                    result = Ok(mapper.Map(Of TaskType)(innerTaskType))
                End If
            End Using
            Return result
        End Function

        'todo add error handling
        <HttpPut(), ClaimsAuthorization(ClaimTypes:="TA")> Public Function SaveTaskType(<FromBody()> ByVal group As TaskType) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim innerTaskType As ITaskType = Nothing
            Dim taskTypeSaver As ITaskTypeSaver
            Dim taskTypeFactory As ITaskTypeFactory
            Dim mapper As IMapper
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                taskTypeFactory = scope.Resolve(Of ITaskTypeFactory)()
                If group.TaskTypeId.Equals(Guid.Empty) Then
                    innerTaskType = taskTypeFactory.Create()
                Else
                    innerTaskType = taskTypeFactory.Get(New Settings(), group.TaskTypeId)
                End If

                If innerTaskType Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    mapper = New Mapper(m_mapperConfiguration)
                    mapper.Map(Of TaskType, ITaskType)(group, innerTaskType)
                    taskTypeSaver = scope.Resolve(Of ITaskTypeSaver)()
                    taskTypeSaver.Save(New Settings(), innerTaskType)
                    result = Ok(mapper.Map(Of TaskType)(innerTaskType))
                End If
            End Using
            Return result
        End Function

        'todo add error handling
        <HttpGet(), ClaimsAuthorization(ClaimTypes:="TA"), Route("api/TaskType/{id}/EventTypes")>
        Function GetEventTypes(ByVal id As Guid, ByVal Optional allEventTypes As Boolean = False) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim taskType As ITaskType
            Dim taskTypeFactory As ITaskTypeFactory
            Dim events As IEnumerable(Of TaskTypeEventType)
            Dim allEventsList As IEnumerable(Of IEventType)
            Dim taskTypeEventTypes As IEnumerable(Of ITaskTypeEventType)
            Dim eventTypeFactory As IEventTypeFactory
            Using scope As ILifetimeScope = Me.ObjectContainer().BeginLifetimeScope
                taskTypeFactory = scope.Resolve(Of ITaskTypeFactory)()
                taskType = taskTypeFactory.Get(New Settings(), id)

                If taskType Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    eventTypeFactory = scope.Resolve(Of IEventTypeFactory)()
                    allEventsList = eventTypeFactory.GetAll(New Settings())
                    taskTypeEventTypes = taskType.GetEventTypes(New Settings())
                    events = From e In (
                                 From e In allEventsList
                                 Select MapTaskTypeEventType(e, taskType, taskTypeEventTypes)
                             )
                             Where allEventTypes = True OrElse e.IsActive = True

                    result = Ok(events)
                End If
            End Using
            Return result
        End Function

        <NonAction> Private Function MapTaskTypeEventType(ByVal [event] As IEventType,
                                                  ByVal taskType As ITaskType,
                                                  ByVal taskTypeEventTypes As IEnumerable(Of ITaskTypeEventType)) As TaskTypeEventType

            Dim taskTypeEventType As ITaskTypeEventType = taskTypeEventTypes.FirstOrDefault(Function(ettt As ITaskTypeEventType) ettt.EventTypeId.Equals([event].EventTypeId))
            Dim result As TaskTypeEventType

            If taskTypeEventType Is Nothing Then
                result = New TaskTypeEventType() With {.EventTypeId = [event].EventTypeId, .IsActive = False, .Title = [event].Title}
            Else
                result = New TaskTypeEventType() With {.EventTypeId = taskTypeEventType.EventTypeId, .IsActive = taskTypeEventType.IsActive, .Title = taskTypeEventType.Title}
            End If
            Return result
        End Function

        'todo add error handling
        <HttpPut(), ClaimsAuthorization(ClaimTypes:="TA"), Route("api/TaskType/{id}/EventTypes")>
        Function SaveEventTypes(ByVal id As Guid, ByVal taskTypeEventTypes As IEnumerable(Of TaskTypeEventType)) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim taskType As ITaskType
            Dim taskTypeFactory As ITaskTypeFactory
            Dim innerTaskTypeEventTypes As IEnumerable(Of ITaskTypeEventType)
            Dim allEventTypes As IEnumerable(Of IEventType)
            Dim eventTypeFactory As IEventTypeFactory
            Dim toUpdate As IEnumerable(Of ITaskTypeEventType)
            Dim toCreate As IEnumerable(Of ITaskTypeEventType)
            Dim saver As ITaskTypeEventTypeSaver
            Using scope As ILifetimeScope = Me.ObjectContainer().BeginLifetimeScope
                taskTypeFactory = scope.Resolve(Of ITaskTypeFactory)()
                taskType = taskTypeFactory.Get(New Settings(), id)

                If taskType Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    eventTypeFactory = scope.Resolve(Of IEventTypeFactory)()
                    allEventTypes = eventTypeFactory.GetAll(New Settings())
                    innerTaskTypeEventTypes = taskType.GetEventTypes(New Settings())

                    toUpdate = From ug In taskTypeEventTypes
                               Join iug In innerTaskTypeEventTypes On ug.EventTypeId Equals iug.EventTypeId
                               Where ug.IsActive <> iug.IsActive
                               Select SetEventIsActive(iug, ug.IsActive)

                    toCreate = From ug In taskTypeEventTypes
                               Where ug.IsActive = True AndAlso innerTaskTypeEventTypes.Any(Function(iug As ITaskTypeEventType) iug.EventTypeId.Equals(ug.EventTypeId)) = False
                               Join g In allEventTypes On ug.EventTypeId Equals g.EventTypeId
                               Select taskType.CreateTaskTypeEventType(g)


                    saver = scope.Resolve(Of ITaskTypeEventTypeSaver)()
                    saver.Save(New Settings, toUpdate.Concat(toCreate))
                    result = Ok("Event Types Updated")
                End If
            End Using
            Return result
        End Function

        <NonAction()> Private Function SetEventIsActive(ByVal eventType As ITaskTypeEventType, ByVal value As Boolean) As ITaskTypeEventType
            eventType.IsActive = value
            Return eventType
        End Function

    End Class
End Namespace