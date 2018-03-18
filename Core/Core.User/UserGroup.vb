Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class UserGroup
    Implements IUserGroup

    Private m_userGroupData As UserGroupData
    Private m_innerGroup As IGroup
    Private m_user As IUser
    Private m_container As IContainer

    Public Sub New(ByVal user As IUser, ByVal group As IGroup, ByVal userGroupData As UserGroupData)
        m_userGroupData = userGroupData
        m_innerGroup = group
        m_user = user
        m_container = ObjectContainer.GetContainer
    End Sub

    Public ReadOnly Property GroupId As Guid Implements IGroup.GroupId
        Get
            Return m_innerGroup.GroupId
        End Get
    End Property

    Public Property IsActive As Boolean Implements IUserGroup.IsActive
        Get
            Return m_userGroupData.IsActive
        End Get
        Set(value As Boolean)
            m_userGroupData.IsActive = value
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

    Public Function GetDataCreator(settings As ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Return New DataCreatorWrapper(Sub() Create(settings))
    End Function

    Private Sub Create(settings As ISettings)
        Dim creator As AbyssalDataProcessor.DataTier.Core.IDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            m_userGroupData.UserId = m_user.UserId
            m_userGroupData.GroupId = m_innerGroup.GroupId
            creator = scope.Resolve(Of UserGroupDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)),
                New TypedParameter(GetType(UserGroupData), m_userGroupData)
            )
        End Using
    End Sub

    Public Function GetDataUpdater(settings As ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataUpdateWrapper(scope.Resolve(Of UserGroupDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)),
                New TypedParameter(GetType(UserGroupData), m_userGroupData)
            ))
        End Using
    End Function

    Public Sub Save(settings As ISettings) Implements IUserGroup.Save
        Dim creator As Framework.IDataCreator
        Dim updater As Framework.IDataUpdater
        If m_userGroupData.DataStateManager.GetState(m_userGroupData) = DataTier.Utilities.IDataStateManager(Of UserGroupData).enumState.New Then
            creator = GetDataCreator(settings)
            creator.Create()
        ElseIf m_userGroupData.DataStateManager.GetState(m_userGroupData) = DataTier.Utilities.IDataStateManager(Of UserGroupData).enumState.Updated Then
            updater = GetDataUpdater(settings)
            updater.Update()
        End If
    End Sub
End Class
