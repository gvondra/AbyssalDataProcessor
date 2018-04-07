Imports AbyssalDataProcessor.DataTier.Client
Imports AbyssalDataProcessor.DataTier.Utilities
Imports Autofac
Friend NotInheritable Class ObjectContainer

    Private Shared m_container As IContainer

    Shared Sub New()
        Dim builder As New ContainerBuilder()

        builder.RegisterType(Of UserDataFactory).As(Of IUserDataFactory)()
        builder.RegisterType(Of UserDataSaver).Keyed(Of IDataCreator)("UserDataSaver")
        builder.RegisterType(Of UserDataSaver).Keyed(Of IDataUpdater)("UserDataSaver")
        builder.RegisterType(Of UserAccountDataSaver).Keyed(Of IDataCreator)("UserAccountDataSaver")

        m_container = builder.Build()
    End Sub

    Public Shared Function GetContainer() As IContainer
        Return m_container
    End Function
End Class
