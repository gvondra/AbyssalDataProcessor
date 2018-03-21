Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class TaskController
        Inherits ControllerBase

        <HttpGet(), ClaimsAuthorization(ClaimTypes:="TP|TA")> Public Function GetTask(ByVal id As Guid) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim mapper As IMapper
            Dim mapperConfiguration As TaskMapper
            Dim taskFactory As ITaskFactory
            Dim task As Task
            Dim innerTask As ITask = Nothing

            If id.Equals(Guid.Empty) Then
                result = BadRequest("Missing or invalid task id")
            End If

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                If result Is Nothing Then
                    taskFactory = scope.Resolve(Of ITaskFactory)()
                    innerTask = taskFactory.Get(New Settings(), id)
                    If innerTask Is Nothing Then
                        result = NotFound()
                    End If
                End If

                If result Is Nothing Then
                    mapperConfiguration = New TaskMapper
                    mapper = New Mapper(mapperConfiguration.MapperConfiguration)
                    task = mapper.Map(Of Task)(innerTask)
                    result = Ok(task)
                End If
            End Using

            Return result
        End Function

        <HttpGet(), ClaimsAuthorization(ClaimTypes:="TP|TA"), Route("api/Task/{id}/FormIds")> Public Function GetFormIds(ByVal id As Guid) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim taskFactory As ITaskFactory
            Dim formIds As IEnumerable(Of Guid)

            If id.Equals(Guid.Empty) Then
                result = BadRequest("Missing or invalid task id")
            End If

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                If result Is Nothing Then
                    taskFactory = scope.Resolve(Of ITaskFactory)()
                    formIds = taskFactory.GetFormIds(New Settings(), id)
                    result = Ok(formIds)
                End If
            End Using

            Return result
        End Function
    End Class
End Namespace