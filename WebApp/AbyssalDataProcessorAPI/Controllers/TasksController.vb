Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class TasksController
        Inherits ControllerBase

        <HttpGet(), ClaimsAuthorization(ClaimTypes:="TP|TA")> Public Function GetTasks() As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim user As IUser
            Dim userFactory As IUserFactory
            Dim taskFactory As ITaskFactory
            Dim mapper As IMapper
            Dim mapperConfiguration As TaskMapper
            Dim tasks As IEnumerable(Of Task)
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                user = userFactory.Get(CType(Me.User, ClaimsPrincipal))

                mapperConfiguration = New TaskMapper
                mapper = New Mapper(mapperConfiguration.MapperConfiguration)
                taskFactory = scope.Resolve(Of ITaskFactory)()
                tasks = From t In taskFactory.GetByUserId(New Settings(), user.UserId)
                        Select mapper.Map(Of Task)(t)

                result = Ok(tasks)
            End Using

            Return result
        End Function
    End Class
End Namespace