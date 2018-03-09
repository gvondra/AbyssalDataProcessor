Imports System.Web.Http
Imports Swashbuckle.Application

Public Class SwaggerConfig
    Public Shared Sub Register()
        Register(GlobalConfiguration.Configuration)
    End Sub

    Public Shared Sub Register(ByVal config As HttpConfiguration)
        Dim thisAssembly As Reflection.Assembly = GetType(SwaggerConfig).Assembly()

        config.EnableSwagger(
        Sub(c As SwaggerDocsConfig)
            c.SingleApiVersion("v1", "AbyssalDataProcessorAPI")
            c.IncludeXmlComments(System.IO.Path.ChangeExtension(thisAssembly.CodeBase, "xml"))
        End Sub) _
        .EnableSwaggerUi()
    End Sub
End Class
