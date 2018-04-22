Imports AbyssalDataProcessor.Client.Form
Imports AbyssalDataProcessor.Client.User
Imports Autofac
Friend NotInheritable Class ObjectContainer

    Private Shared m_container As IContainer

    Shared Sub New()
        Dim builder As New ContainerBuilder()

        builder.RegisterType(Of UserFactory)().As(Of IUserFactory)()
        builder.RegisterType(Of UserSaver)().As(Of IUserSaver)()

        builder.RegisterType(Of GroupFactory)().As(Of IGroupFactory)()
        builder.RegisterType(Of GroupSaver)().As(Of IGroupSaver)()

        builder.RegisterType(Of FormFactory)().As(Of IFormFactory)()
        builder.RegisterType(Of FormSaver)().As(Of IFormSaver)()

        builder.RegisterType(Of AbyssalDataProcessor.Core.Log.WebMetricSaver)() _
            .As(Of AbyssalDataProcessor.Core.Framework.IWebMetricSaver)()

        m_container = builder.Build()
    End Sub

    Public Shared Function GetContainer() As IContainer
        Return m_container
    End Function
End Class
