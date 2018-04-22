Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class GroupController
        Inherits ControllerBase

        Private Shared m_mapperConfiguration As MapperConfiguration

        Shared Sub New()
            m_mapperConfiguration = New MapperConfiguration(Sub(exp As IMapperConfigurationExpression)
                                                                exp.CreateMap(Of Group, IGroup)()
                                                                exp.CreateMap(Of IGroup, Group)()
                                                            End Sub)
        End Sub

        'todo add error handling
        <HttpGet(), Authorize()> Public Shadows Function GetGroup(ByVal id As Guid) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim innerGroup As IGroup
            Dim groupFactory As IGroupFactory
            Dim mapper As IMapper
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                groupFactory = scope.Resolve(Of IGroupFactory)()
                innerGroup = groupFactory.Get(New Settings(), id, GetOrganizationId)

                If innerGroup Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    mapper = New Mapper(m_mapperConfiguration)
                    result = Ok(mapper.Map(Of Group)(innerGroup))
                End If
            End Using
            Return result
        End Function

        'todo add error handling
        <HttpPut(), Authorize()> Public Function SaveGroup(<FromBody()> ByVal group As Group) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim innerGroup As IGroup = Nothing
            Dim groupSaver As IGroupSaver
            Dim groupFactory As IGroupFactory
            Dim mapper As IMapper
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                groupFactory = scope.Resolve(Of IGroupFactory)()
                If group.GroupId.Equals(Guid.Empty) Then
                    innerGroup = groupFactory.Create(Me.GetOrganizationId)
                Else
                    innerGroup = groupFactory.Get(New Settings(), group.GroupId, Me.GetOrganizationId)
                End If

                If innerGroup Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    mapper = New Mapper(m_mapperConfiguration)
                    mapper.Map(Of Group, IGroup)(group, innerGroup)
                    groupSaver = scope.Resolve(Of IGroupSaver)()
                    groupSaver.Save(New Settings(), innerGroup)
                    result = Ok(mapper.Map(Of Group)(innerGroup))
                End If
            End Using
            Return result
        End Function
    End Class
End Namespace