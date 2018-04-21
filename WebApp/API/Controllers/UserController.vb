Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class UserController
        Inherits ControllerBase

        Private Shared m_mapperConfiguration As MapperConfiguration

        Shared Sub New()
            m_mapperConfiguration = New MapperConfiguration(
                Sub(exp As IMapperConfigurationExpression)
                    exp.CreateMap(Of IUser, User)()
                End Sub
            )
        End Sub

        <HttpGet(), ClientAuthorization()> Public Function GetBySubscriberId(ByVal subscriberId As String) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim userFactory As IUserFactory
            Dim innerUser As IUser
            Dim mapper As IMapper
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                innerUser = userFactory.GetBySubscriberId(New Settings(), GetOrganizationId, subscriberId)
                If innerUser IsNot Nothing Then
                    mapper = New Mapper(m_mapperConfiguration)
                    result = Ok(mapper.Map(Of User)(innerUser))
                Else
                    result = Ok()
                End If
            End Using

            Return result
        End Function
    End Class
End Namespace