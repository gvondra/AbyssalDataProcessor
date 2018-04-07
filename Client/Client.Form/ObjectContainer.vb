Imports AbyssalDataProcessor.DataTier.Client
Imports AbyssalDataProcessor.DataTier.Utilities
Imports Autofac
Friend NotInheritable Class ObjectContainer

    Private Shared m_container As IContainer

    Shared Sub New()
        Dim builder As New ContainerBuilder()

        builder.RegisterType(Of FormDataSaver).Keyed(Of IDataCreator)("FormDataSaver")
        'builder.RegisterType(Of FormDataSaver).Keyed(Of IDataUpdater)("FormDataSaver")

        m_container = builder.Build()
    End Sub

    Public Shared Function GetContainer() As IContainer
        Return m_container
    End Function
End Class
