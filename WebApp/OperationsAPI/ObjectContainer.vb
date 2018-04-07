Imports AbyssalDataProcessor.Core.Event
Imports AbyssalDataProcessor.Core.Form
Imports AbyssalDataProcessor.Core.Log
Imports AbyssalDataProcessor.Core.Organization
Imports AbyssalDataProcessor.Core.Task
Imports AbyssalDataProcessor.Core.User
Imports Autofac
Friend NotInheritable Class ObjectContainer

    Private Shared m_container As IContainer

    Shared Sub New()
        Dim builder As New ContainerBuilder()

        builder.RegisterType(Of Settings)().As(Of AbyssalDataProcessor.Core.Utilities.Framework.ISettings)()

        builder.RegisterType(Of TaskTypeFactory)().As(Of ITaskTypeFactory)()
        builder.RegisterType(Of TaskTypeSaver)().As(Of ITaskTypeSaver)()
        builder.RegisterType(Of TaskTypeEventTypeSaver)().As(Of ITaskTypeEventTypeSaver)()

        builder.RegisterType(Of TaskFactory)().As(Of ITaskFactory)()
        builder.RegisterType(Of TaskSaver)().As(Of ITaskSaver)()
        builder.RegisterType(Of UnassignedTaskFactory)().As(Of IUnassignedTaskFactory)()

        builder.RegisterType(Of GroupFactory)().As(Of IGroupFactory)()
        builder.RegisterType(Of GroupSaver)().As(Of IGroupSaver)()

        builder.RegisterType(Of AbyssalDataProcessor.Core.User.UserFactory)().As(Of AbyssalDataProcessor.Core.Framework.IUserFactory)()
        builder.RegisterType(Of UserFactory)().As(Of IUserFactory)()

        builder.RegisterType(Of UserSaver)().As(Of IUserSaver)()
        builder.RegisterType(Of UserUpdater)().As(Of IUserUpdater)()
        builder.RegisterType(Of UserGroupSaver)().As(Of IUserGroupSaver)()
        builder.RegisterType(Of TaskTypeGroupSaver)().As(Of ITaskTypeGroupSaver)()

        builder.RegisterType(Of FormSerializerFactory)().As(Of IFormSerializerFactory)()
        builder.RegisterType(Of FormFactory)().As(Of IFormFactory)()
        builder.RegisterType(Of FormSaver)().As(Of IFormSaver)()
        builder.RegisterType(Of FormContentTransformFactory)().As(Of IFormContentTransormFactory)()

        builder.RegisterType(Of OrganizationFactory)().As(Of IOrganizationFactory)()
        builder.RegisterType(Of OrganizationSaver)().As(Of IOrganizationSaver)()

        builder.RegisterType(Of EventSaver)().As(Of IEventSaver)()
        builder.RegisterType(Of EventFactory)().As(Of IEventFactory)()
        builder.RegisterType(Of EventTypeFactory)().As(Of IEventTypeFactory)()
        builder.RegisterType(Of EventTypeSaver)().As(Of IEventTypeSaver)()

        builder.RegisterType(Of EventTriggerFactory)().As(Of IEventTriggerFactory)()

        builder.RegisterType(Of WebMetricSaver)().As(Of IWebMetricSaver)()
        builder.RegisterType(Of WebMetricFactory)().As(Of IWebMetricFactory)()

        m_container = builder.Build()
    End Sub

    Public Shared Function GetContainer() As IContainer
        Return m_container
    End Function
End Class
