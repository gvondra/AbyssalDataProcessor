Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class UserController
        Inherits ControllerBase

        Private Shared UserMapper As UserMapper

        Shared Sub New()
            UserMapper = New UserMapper
        End Sub

        'todo add error handling
        <HttpGet(), Authorize()> Public Shadows Function GetUser() As IHttpActionResult
            Dim userFactory As IUserFactory
            Dim u As IUser = Nothing
            Dim mapper As IMapper
            Dim result As IHttpActionResult = Nothing

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                u = userFactory.Get(CType(Me.User, ClaimsPrincipal))
            End Using

            If u Is Nothing Then
                result = NotFound()
            End If

            If result Is Nothing Then
                mapper = New Mapper(UserMapper.MapperConfiguration)
                result = Ok(mapper.Map(Of User)(u))
            End If

            Return result
        End Function

        'todo add error handling
        <HttpGet(), Authorize()> Public Shadows Function GetUser(ByVal id As Guid) As IHttpActionResult
            Dim userFactory As IUserFactory
            Dim requestRole As enumRole = RoleProcessor.GetRoleFlags(CType(Me.User, ClaimsPrincipal))
            Dim currentUser As IUser = Nothing
            Dim u As IUser = Nothing
            Dim mapper As IMapper
            Dim result As IHttpActionResult = Nothing

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                u = userFactory.Get(New Settings(), id)
            End Using

            If u Is Nothing Then
                If (requestRole And enumRole.UserAdministrator) = enumRole.UserAdministrator Then
                    result = NotFound()
                Else
                    result = Unauthorized()
                End If
            End If

            If result Is Nothing _
                    AndAlso (requestRole And enumRole.UserAdministrator) <> enumRole.UserAdministrator Then

                currentUser = userFactory.Get(CType(Me.User, ClaimsPrincipal))
                If id.Equals(currentUser.UserId) = False Then
                    result = Unauthorized()
                End If
            End If

            If result Is Nothing Then
                mapper = New Mapper(UserMapper.MapperConfiguration)
                result = Ok(mapper.Map(Of User)(u))
            End If

            Return result
        End Function

        'todo add error handling
        <HttpPut(), Authorize()> Public Function UpdateUser(<FromBody()> ByVal user As User) As IHttpActionResult
            Dim userFactory As IUserFactory
            Dim requestRole As enumRole = RoleProcessor.GetRoleFlags(CType(Me.User, ClaimsPrincipal))
            Dim currentUser As IUser = Nothing
            Dim u As IUser = Nothing
            Dim mapper As IMapper
            Dim result As IHttpActionResult = Nothing
            Dim userSaver As IUserSaver

            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()

                If user.UserId.Equals(Guid.Empty) = False Then
                    u = userFactory.Get(New Settings(), user.UserId)
                End If

                If u Is Nothing Then
                    If (requestRole And enumRole.UserAdministrator) = enumRole.UserAdministrator Then
                        result = NotFound()
                    Else
                        result = Unauthorized()
                    End If
                End If

                If result Is Nothing _
                        AndAlso (requestRole And enumRole.UserAdministrator) <> enumRole.UserAdministrator Then

                    currentUser = userFactory.Get(CType(Me.User, ClaimsPrincipal))
                    If user.UserId.Equals(currentUser.UserId) = False Then
                        result = Unauthorized()
                    End If
                End If

                If result Is Nothing Then
                    mapper = New Mapper(UserMapper.MapperConfiguration)
                    mapper.Map(Of User, IUser)(user, u)
                    userSaver = scope.Resolve(Of IUserSaver)()
                    userSaver.Save(New Settings(), u)
                    result = Ok(mapper.Map(Of User)(u))
                End If
            End Using
            Return result
        End Function
    End Class
End Namespace