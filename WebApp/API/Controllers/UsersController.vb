Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class UsersController
        Inherits ControllerBase

        Private Shared m_userMapper As UserMapper

        Shared Sub New()
            m_userMapper = New UserMapper
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
                    mapper = New Mapper(m_userMapper.MapperConfiguration)
                    mapper.Map(Of User, IUser)(request.User, innerUser)
                    saver = scope.Resolve(Of IUserSaver)()
                    saver.Save(New Settings(), innerUser, request.SubscriberId)
                    result = Ok(mapper.Map(Of User)(innerUser))
                End If
            End Using

            Return result
        End Function

        <HttpGet(), Authorize(), Route("api/Users/Search")>
        Function Search(ByVal s As String) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim userId As Guid?
            Dim userFactory As IUserFactory
            Dim innerUsers As IEnumerable(Of IUser) = Nothing
            Dim u As IUser
            Dim users As IEnumerable(Of User)
            Dim mapper As IMapper

            If String.IsNullOrEmpty(s) Then
                result = BadRequest("Missing search text")
            End If

            If result Is Nothing Then
                Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                    userFactory = scope.Resolve(Of IUserFactory)()
                    userId = SearchToGuid(s)
                    If userId.HasValue Then
                        u = userFactory.Get(New Settings(), GetOrganizationId, userId.Value)
                        If u IsNot Nothing Then
                            innerUsers = {u}
                        Else
                            innerUsers = New List(Of IUser)()
                        End If
                    Else
                        innerUsers = userFactory.Search(New Settings(), GetOrganizationId, s)
                    End If
                End Using
            End If

            If result Is Nothing Then
                mapper = New Mapper(m_userMapper.MapperConfiguration)
                users = From y In innerUsers.Select(Of User)(Function(x As IUser) mapper.Map(Of User)(x))
                result = Ok(users)
            End If

            Return result
        End Function

        Private Function SearchToGuid(ByVal s As String) As Guid?
            Dim g As Guid? = Nothing
            Dim h As Guid
            s = Regex.Replace(s, "[^0-9A-Fa-f]+", String.Empty)
            If Regex.IsMatch(s, "[0-9A-Fa-f]{32}") Then
                If Guid.TryParse(s, h) Then
                    g = h
                End If
            End If
            Return g
        End Function
    End Class
End Namespace