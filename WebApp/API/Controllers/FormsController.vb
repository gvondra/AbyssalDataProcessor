Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Namespace Controllers
    Public Class FormsController
        Inherits ControllerBase

        Private Shared m_mapperConfiguration As MapperConfiguration

        Shared Sub New()
            m_mapperConfiguration = New MapperConfiguration(
                Sub(exp As IMapperConfigurationExpression)
                    exp.CreateMap(Of IForm, Form)()
                End Sub
            )
        End Sub

        <HttpPost(), ClientAuthorization()> Public Function CreateForm(<FromBody> ByVal request As CreateFormRequest) As IHttpActionResult
            Dim userFactory As IUserFactory
            Dim user As IUser = Nothing
            Dim formFactory As IFormFactory
            Dim form As IForm
            Dim organizationId As Guid
            Dim response As IHttpActionResult = Nothing
            Dim formSaver As IFormSaver
            Dim mapper As imapper
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                organizationId = GetOrganizationId()

                If request Is Nothing Then
                    response = BadRequest("Missing request")
                End If

                If response IsNot Nothing AndAlso request.UserId.Equals(Guid.Empty) Then
                    response = BadRequest("Missing or Invalid User Id")
                End If

                If response Is Nothing Then
                    userFactory = scope.Resolve(Of IUserFactory)()
                    user = userFactory.Get(New Settings(), GetOrganizationId, request.UserId)
                    If user Is Nothing Then
                        response = BadRequest("User not found")
                    End If
                End If

                If response Is Nothing Then
                    formFactory = scope.Resolve(Of IFormFactory)()
                    form = formFactory.CreateForm(GetOrganizationId, user, request.Type, request.Style, request.Content)
                    formSaver = scope.Resolve(Of IFormSaver)()
                    formSaver.Create(New Settings(), form)

                    ' todo trigger event

                    mapper = New Mapper(m_mapperConfiguration)
                    response = Ok(mapper.Map(Of Form)(form))
                End If
            End Using
            Return response
        End Function
    End Class
End Namespace