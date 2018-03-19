Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class TaskTypeEventType
    Implements ITaskTypeEventType

    Private m_eventTypeTaskTypeData As EventTypeTaskTypeData
    Private m_innerEventType As IEventType
    Private m_taskType As ITaskType
    Private m_container As IContainer

    Friend Sub New(ByVal taskType As ITaskType, ByVal eventType As IEventType, data As EventTypeTaskTypeData)
        m_eventTypeTaskTypeData = data
        m_innerEventType = eventType
        m_taskType = taskType
        m_container = ObjectContainer.GetContainer()
    End Sub

    Public ReadOnly Property EventTypeId As Int16 Implements IEventType.EventTypeId
        Get
            Return m_innerEventType.EventTypeId
        End Get
    End Property

    Public Property IsActive As Boolean Implements ITaskTypeEventType.IsActive
        Get
            Return m_eventTypeTaskTypeData.IsActive
        End Get
        Set(value As Boolean)
            m_eventTypeTaskTypeData.IsActive = value
        End Set
    End Property

    Public Property Title As String Implements IEventType.Title
        Get
            Return m_innerEventType.Title
        End Get
        Set(value As String)
            m_innerEventType.Title = value
        End Set
    End Property

    Public Sub Save(settings As ISettings) Implements ITaskTypeEventType.Save
        Dim creator As Framework.IDataCreator
        Dim updater As Framework.IDataUpdater
        If m_eventTypeTaskTypeData.DataStateManager.GetState(m_eventTypeTaskTypeData) = DataTier.Utilities.IDataStateManager(Of UserGroupData).enumState.New Then
            creator = GetDataCreator(settings)
            creator.Create()
        ElseIf m_eventTypeTaskTypeData.DataStateManager.GetState(m_eventTypeTaskTypeData) = DataTier.Utilities.IDataStateManager(Of UserGroupData).enumState.Updated Then
            updater = GetDataUpdater(settings)
            updater.Update()
        End If
    End Sub

    Public Function GetDataCreator(settings As ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Return New DataCreatorWrapper(Sub() Create(settings))
    End Function

    Private Sub Create(ByVal settings As Framework.ISettings)
        Dim creator As Framework.IDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            creator = New DataCreatorWrapper(scope.Resolve(Of EventTypeTaskTypeDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)),
                New TypedParameter(GetType(EventTypeTaskTypeData), m_eventTypeTaskTypeData)
            ))
            creator.Create()
        End Using
    End Sub

    Public Function GetDataUpdater(settings As ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataUpdateWrapper(scope.Resolve(Of EventTypeTaskTypeDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)),
                New TypedParameter(GetType(EventTypeTaskTypeData), m_eventTypeTaskTypeData)
            ))
        End Using
    End Function
End Class
