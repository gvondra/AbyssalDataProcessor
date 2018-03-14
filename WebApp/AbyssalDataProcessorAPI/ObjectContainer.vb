Imports AbyssalDataProcessor.Core.Event
Imports AbyssalDataProcessor.Core.Form
Imports AbyssalDataProcessor.Core.Log
Imports AbyssalDataProcessor.Core.User
Imports Autofac
Friend NotInheritable Class ObjectContainer

    Private Shared m_container As IContainer

    Shared Sub New()
        Dim builder As New ContainerBuilder()

        builder.RegisterType(Of Settings)().As(Of ISettings)()

        builder.RegisterType(Of AbyssalDataProcessor.Core.User.UserFactory)().As(Of AbyssalDataProcessor.Core.Framework.IUserFactory)()
        builder.RegisterType(Of UserFactory)().As(Of IUserFactory)()

        builder.RegisterType(Of UserSaver)().As(Of IUserSaver)()
        builder.RegisterType(Of UserUpdater)().As(Of IUserUpdater)()

        builder.RegisterType(Of FormSerializerFactory)().As(Of IFormSerializerFactory)()
        builder.RegisterType(Of FormFactory)().As(Of IFormFactory)()
        builder.RegisterType(Of FormSaver)().As(Of IFormSaver)()

        builder.RegisterType(Of EventSaver)().As(Of IEventSaver)()
        builder.RegisterType(Of EventFactory)().As(Of IEventFactory)()
        builder.RegisterType(Of EventTypeFactory)().As(Of IEventTypeFactory)()
        builder.RegisterType(Of EventTypeSaver)().As(Of IEventTypeSaver)()

        builder.RegisterType(Of WebMetricSaver)().As(Of IWebMetricSaver)()

        m_container = builder.Build()
    End Sub

    Public Shared Function GetContainer() As IContainer
        Return m_container
    End Function
End Class
