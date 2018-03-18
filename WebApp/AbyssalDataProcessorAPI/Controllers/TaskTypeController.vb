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
    End Class
End Namespace