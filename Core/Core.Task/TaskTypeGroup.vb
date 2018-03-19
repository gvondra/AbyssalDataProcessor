Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class TaskTypeGroup
    Implements ITaskTypeGroup

    Private m_taskTypeGroupData As TaskTypeGroupData
    Private m_innerGroup As IGroup
    Private m_taskType As ITaskType
    Private m_container As IContainer

    Friend Sub New(ByVal taskType As ITaskType, ByVal group As IGroup, ByVal data As TaskTypeGroupData)
        m_taskTypeGroupData = data
        m_innerGroup = group
        m_taskType = taskType
        m_container = ObjectContainer.GetContainer
    End Sub

    Public ReadOnly Property GroupId As Guid Implements IGroup.GroupId
        Get
            Return m_innerGroup.GroupId
        End Get
    End Property

    Public Property IsActive As Boolean Implements ITaskTypeGroup.IsActive
        Get
            Return m_taskTypeGroupData.IsActive
        End Get
        Set(value As Boolean)
            m_taskTypeGroupData.IsActive = value
        End Set
    End Property

    Public Property Name As String Implements IGroup.Name
        Get
            Return m_innerGroup.Name
        End Get
        Set(value As String)
            m_innerGroup.Name = value
        End Set
    End Property

    Public Sub Save(settings As ISettings) Implements ITaskTypeGroup.Save
        Dim creator As Framework.IDataCreator
        Dim updater As Framework.IDataUpdater
        If m_taskTypeGroupData.DataStateManager.GetState(m_taskTypeGroupData) = DataTier.Utilities.IDataStateManager(Of UserGroupData).enumState.New Then
            creator = GetDataCreator(settings)
            creator.Create()
        ElseIf m_taskTypeGroupData.DataStateManager.GetState(m_taskTypeGroupData) = DataTier.Utilities.IDataStateManager(Of UserGroupData).enumState.Updated Then
            updater = GetDataUpdater(settings)
            updater.Update()
        End If
    End Sub

    Public Function GetDataCreator(settings As ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Return New DataCreatorWrapper(Sub() Create(settings))
    End Function

    Private Sub Create(ByVal settings As ISettings)
        Dim creator As Framework.IDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            m_taskTypeGroupData.TaskTypeId = m_taskType.TaskTypeId
            m_taskTypeGroupData.GroupId = m_innerGroup.GroupId
            creator = New DataCreatorWrapper(scope.Resolve(Of TaskTypeGroupDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)),
                New TypedParameter(GetType(TaskTypeGroupData), m_taskTypeGroupData)
            ))
            creator.Create()
        End Using
    End Sub

    Public Function GetDataUpdater(settings As ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataUpdateWrapper(scope.Resolve(Of TaskTypeGroupDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)),
                New TypedParameter(GetType(TaskTypeGroupData), m_taskTypeGroupData)
            ))
        End Using
    End Function
End Class
