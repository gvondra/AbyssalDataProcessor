Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class OrganizationsController
        Inherits ControllerBase

        Private Shared m_mapperConfiguration As MapperConfiguration

        Shared Sub New()
            m_mapperConfiguration = New MapperConfiguration(Sub(exp As IMapperConfigurationExpression)
                                                                exp.CreateMap(Of IOrganization, Organization)()
                                                            End Sub)
        End Sub

        <HttpGet(), ClaimsAuthorization(ClaimTypes:="OA"), Route("api/Organizations/Search")>
        Function Search(ByVal s As String) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim organizationId As Guid?
            Dim organizationFactory As IOrganizationFactory
            Dim innerOrganizations As IEnumerable(Of IOrganization) = Nothing
            Dim o As IOrganization
            Dim organizations As IEnumerable(Of Organization)
            Dim mapper As IMapper

            If String.IsNullOrEmpty(s) Then
                result = BadRequest("Missing search text")
            End If

            If result Is Nothing Then
                Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                    organizationFactory = scope.Resolve(Of IOrganizationFactory)()
                    organizationId = SearchToGuid(s)
                    If organizationId.HasValue Then
                        o = organizationFactory.Get(New Settings(), organizationId.Value)
                        If o IsNot Nothing Then
                            innerOrganizations = {o}
                        Else
                            innerOrganizations = {}
                        End If
                    Else
                        innerOrganizations = organizationFactory.Search(New Settings(), s)
                    End If
                End Using
            End If

            If result Is Nothing Then
                mapper = New Mapper(m_mapperConfiguration)
                organizations = From y In innerOrganizations.Select(Of Organization)(Function(x As IOrganization) mapper.Map(Of Organization)(x))
                result = Ok(organizations)
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