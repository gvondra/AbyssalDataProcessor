Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class UserController
        Inherits ControllerBase

        Private Shared m_userMapper As UserMapper

        Shared Sub New()
            m_userMapper = New UserMapper
        End Sub

        'todo add error handling
        <HttpGet(), Authorize()> Public Shadows Function GetUser(ByVal id As Guid) As IHttpActionResult
            Dim userFactory As IUserFactory
            Dim u As IUser = Nothing
            Dim mapper As IMapper
            Dim result As IHttpActionResult = Nothing

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                u = userFactory.Get(New Settings(), GetOrganizationId, id)
            End Using

            If u Is Nothing Then
                result = NotFound()
            End If

            If result Is Nothing Then
                mapper = New Mapper(m_userMapper.MapperConfiguration)
                result = Ok(mapper.Map(Of User)(u))
            End If

            Return result
        End Function

        'todo add error handling
        <HttpPut(), Authorize()> Public Function UpdateUser(<FromBody()> ByVal user As User) As IHttpActionResult
            Dim userFactory As IUserFactory
            Dim currentUser As IUser = Nothing
            Dim u As IUser = Nothing
            Dim mapper As IMapper
            Dim result As IHttpActionResult = Nothing
            Dim userSaver As IUserSaver

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()

                If user.UserId.Equals(Guid.Empty) = False Then
                    u = userFactory.Get(New Settings(), GetOrganizationId, user.UserId)
                End If

                If u Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    mapper = New Mapper(m_userMapper.MapperConfiguration)
                    mapper.Map(Of User, IUser)(user, u)
                    userSaver = scope.Resolve(Of IUserSaver)()
                    userSaver.Save(New Settings(), u)
                    result = Ok(mapper.Map(Of User)(u))
                End If
            End Using
            Return result
        End Function

        <HttpGet(), ClientAuthorization()> Public Function GetBySubscriberId(ByVal subscriberId As String) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim userFactory As IUserFactory
            Dim innerUser As IUser
            Dim mapper As IMapper
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                innerUser = userFactory.GetBySubscriberId(New Settings(), GetOrganizationId, subscriberId)
                If innerUser IsNot Nothing Then
                    mapper = New Mapper(m_userMapper.MapperConfiguration)
                    result = Ok(mapper.Map(Of User)(innerUser))
                Else
                    result = Ok()
                End If
            End Using

            Return result
        End Function
    End Class
End Namespace