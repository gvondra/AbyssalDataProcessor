Imports Autofac
Imports AutoMapper
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    <MetricsLog()>
    Public Class OrganizationController
        Inherits ControllerBase

        Private Shared m_mapperConfiguration As MapperConfiguration

        Shared Sub New()
            m_mapperConfiguration = New MapperConfiguration(Sub(exp As IMapperConfigurationExpression)
                                                                exp.CreateMap(Of Organization, IOrganization)()
                                                                exp.CreateMap(Of IOrganization, Organization)()
                                                            End Sub)
        End Sub


        'todo add error handling
        <HttpPut(), ClaimsAuthorization(ClaimTypes:="OA")> Public Function OrganizationGroup(<FromBody()> ByVal organization As Organization) As IHttpActionResult
            Dim result As IHttpActionResult = Nothing
            Dim innerOrganization As IOrganization = Nothing
            Dim organizationSaver As IOrganizationSaver
            Dim organizationFactory As IOrganizationFactory
            Dim mapper As IMapper
            Using scope As ILifetimeScope = Me.ObjectContainer.BeginLifetimeScope
                organizationFactory = scope.Resolve(Of IOrganizationFactory)()
                If organization.OrganizationId.Equals(Guid.Empty) Then
                    innerOrganization = organizationFactory.Create()
                Else
                    innerOrganization = organizationFactory.Get(New Settings(), organization.OrganizationId)
                End If

                If innerOrganization Is Nothing Then
                    result = NotFound()
                End If

                If result Is Nothing Then
                    mapper = New Mapper(m_mapperConfiguration)
                    mapper.Map(Of Organization, IOrganization)(organization, innerOrganization)
                    organizationSaver = scope.Resolve(Of IOrganizationSaver)()
                    organizationSaver.Save(New Settings(), innerOrganization)
                    result = Ok(mapper.Map(Of Organization)(innerOrganization))
                End If
            End Using
            Return result
        End Function
    End Class
End Namespace