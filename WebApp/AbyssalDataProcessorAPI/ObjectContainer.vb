Imports AbyssalDataProcessor.Core.User
Imports Autofac
Friend NotInheritable Class ObjectContainer

    Private Shared m_container As IContainer

    Shared Sub New()
        Dim builder As New ContainerBuilder()

        builder.RegisterType(Of AbyssalDataProcessor.Core.User.UserFactory)().As(Of AbyssalDataProcessor.Core.Framework.IUserFactory)()
        builder.RegisterType(Of UserFactory)().As(Of IUserFactory)()

        m_container = builder.Build()
    End Sub

    Public Shared Function GetContainer() As IContainer
        Return m_container
    End Function
End Class
