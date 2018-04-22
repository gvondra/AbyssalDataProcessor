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


        'todo add error handling
        <HttpGet(), Authorize(), Route("api/User/{id}/Groups")>
        Function GetUserGroups(ByVal id As Guid, ByVal Optional allGroups As Boolean = False) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim user As IUser
            Dim userFactory As IUserFactory
            Dim groups As IEnumerable(Of UserGroup)
            Dim allGroupsList As IEnumerable(Of IGroup)
            Dim userGroups As IEnumerable(Of IUserGroup)
            Dim groupFactory As IGroupFactory
            Using scope As ILifetimeScope = Me.ObjectContainer().BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                user = userFactory.Get(New Settings(), GetOrganizationId, id)

                If user Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    groupFactory = scope.Resolve(Of IGroupFactory)()
                    allGroupsList = groupFactory.GetAll(New Settings(), GetOrganizationId)
                    userGroups = user.GetGroups(New Settings())
                    groups = From g In (
                                 From g In allGroupsList
                                 Select MapUserGroup(g, user, userGroups)
                             )
                             Where allGroups = True OrElse g.IsActive = True

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
        <HttpPut(), Authorize(), Route("api/User/{id}/Groups")>
        Function SaveUserGroups(ByVal id As Guid, ByVal userGroups As IEnumerable(Of UserGroup)) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim user As IUser
            Dim userFactory As IUserFactory
            Dim innerUserGroups As IEnumerable(Of IUserGroup)
            Dim allGroups As IEnumerable(Of IGroup)
            Dim groupFactory As IGroupFactory
            Dim toUpdate As IEnumerable(Of IUserGroup)
            Dim toCreate As IEnumerable(Of IUserGroup)
            Dim saver As IUserGroupSaver
            Using scope As ILifetimeScope = Me.ObjectContainer().BeginLifetimeScope
                userFactory = scope.Resolve(Of IUserFactory)()
                user = userFactory.Get(New Settings(), GetOrganizationId, id)

                If user Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    groupFactory = scope.Resolve(Of IGroupFactory)()
                    allGroups = groupFactory.GetAll(New Settings(), GetOrganizationId)
                    innerUserGroups = user.GetGroups(New Settings())

                    toUpdate = From ug In userGroups
                               Join iug In innerUserGroups On ug.GroupId Equals iug.GroupId
                               Where ug.IsActive <> iug.IsActive
                               Select SetGroupIsActive(iug, ug.IsActive)

                    toCreate = From ug In userGroups
                               Where ug.IsActive = True AndAlso innerUserGroups.Any(Function(iug As IUserGroup) iug.GroupId.Equals(ug.GroupId)) = False
                               Join g In allGroups On ug.GroupId Equals g.GroupId
                               Select user.CreateUserGroup(g)


                    saver = scope.Resolve(Of IUserGroupSaver)()
                    saver.Save(New Settings, toUpdate.Concat(toCreate))
                    result = Ok("Groups Updated")
                End If
            End Using
            Return result
        End Function

        <NonAction()> Private Function SetGroupIsActive(ByVal group As IUserGroup, ByVal value As Boolean) As IUserGroup
            group.IsActive = value
            Return group
        End Function
    End Class
End Namespace