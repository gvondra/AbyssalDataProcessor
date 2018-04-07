Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class UsersController
        Inherits ControllerBase

        Private Shared m_mapperConfiguration As MapperConfiguration

        Shared Sub New()
            m_mapperConfiguration = New MapperConfiguration(
                Sub(exp As IMapperConfigurationExpression)
                    exp.CreateMap(Of User, IUser)()
                    exp.CreateMap(Of IUser, User)()
                End Sub
            )
        End Sub

        <HttpPost(), ClientAuthorization()> Public Function CreateUser(<FromBody> request As CreateUser) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim innerUser As IUser
            Dim userFactory As IUserFactory
            Dim mapper As IMapper
            Dim saver As IUserSaver
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()

                If request Is Nothing OrElse request.User Is Nothing OrElse String.IsNullOrEmpty(request.SubscriberId) Then
                    result = BadRequest("Missing or invalid request")
                End If

                If result Is Nothing Then
                    innerUser = userFactory.GetBySubscriberId(New Settings(), GetOrganizationId, request.SubscriberId)
                    If innerUser IsNot Nothing Then
                        result = BadRequest("User already exists for given subscriber id")
                    End If
                End If

                If result Is Nothing Then
                    innerUser = userFactory.Create(GetOrganizationId)
                    mapper = New Mapper(m_mapperConfiguration)
                    mapper.Map(Of User, IUser)(request.User, innerUser)
                    saver = scope.Resolve(Of IUserSaver)()
                    saver.Save(New Settings(), innerUser, request.SubscriberId)
                    result = Ok(mapper.Map(Of User)(innerUser))
                End If
            End Using

            Return result
        End Function
    End Class
End Namespace