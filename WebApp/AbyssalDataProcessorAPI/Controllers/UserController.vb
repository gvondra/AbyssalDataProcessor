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

        'todo add error handling
        <HttpGet(), ClaimsAuthorization(ClaimTypes:="UA"), Route("api/User/{id}/Groups")>
        Function GetUserGroups(ByVal id As Guid) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim user As IUser
            Dim userFactory As IUserFactory
            Dim groups As IEnumerable(Of UserGroup)
            Dim allGroups As IEnumerable(Of IGroup)
            Dim userGroups As IEnumerable(Of IUserGroup)
            Dim groupFactory As IGroupFactory
            Using scope As ILifetimeScope = Me.ObjectContainer().BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                user = userFactory.Get(New Settings(), id)

                If user Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    groupFactory = scope.Resolve(Of IGroupFactory)()
                    allGroups = groupFactory.GetAll(New Settings())
                    userGroups = user.GetGroups(New Settings())
                    groups = From g In allGroups
                             Select MapUserGroup(g, user, userGroups)

                    result = Ok(groups)
                End If
            End Using
            Return result
        End Function

        <NonAction> Private Function MapUserGroup(ByVal group As IGroup,
                                                  ByVal user As IUser,
                                                  ByVal userGroups As IEnumerable(Of IUserGroup)) As UserGroup

            Dim userGroup As IUserGroup = userGroups.FirstOrDefault(Function(ug As IUserGroup) ug.GroupId.Equals(group.GroupId))
            Dim result As UserGroup

            If userGroup Is Nothing Then
                result = New UserGroup() With {.GroupId = group.GroupId, .IsActive = False, .Name = group.Name}
            Else
                result = New UserGroup() With {.GroupId = userGroup.GroupId, .IsActive = userGroup.IsActive, .Name = userGroup.Name}
            End If
            Return result
        End Function

        'todo add error handling
        <HttpPost(), ClaimsAuthorization(ClaimTypes:="UA"), Route("api/User/{id}/Groups")>
        Function SaveUserGroups(ByVal id As Guid, ByVal userGroups As IEnumerable(Of UserGroup)) As IHttpActionResult

        End Function
    End Class
End Namespace