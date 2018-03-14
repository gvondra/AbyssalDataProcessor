Imports AbyssalDataProcessor.DataTier.Core
Imports Autofac
Friend NotInheritable Class ObjectContainer

    Private Shared m_container As IContainer

    Shared Sub New()
        Dim builder As New ContainerBuilder()

        builder.RegisterType(Of EventTypeDataFactory).As(Of IEventTypeDataFactory)()
        builder.RegisterType(Of EventTypeDataSaver)()
        builder.RegisterType(Of EventDataSaver)()

        m_container = builder.Build()
    End Sub

    Public Shared Function GetContainer() As IContainer
        Return m_container
    End Function
End Class
