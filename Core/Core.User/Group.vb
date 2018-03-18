Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class Group
    Implements IGroup

    Private m_groupData As GroupData
    Private m_container As IContainer

    Friend Sub New(ByVal groupData As GroupData)
        m_groupData = groupData
        m_container = ObjectContainer.GetContainer()
    End Sub

    Public ReadOnly Property GroupId As Guid Implements IGroup.GroupId
        Get
            Return m_groupData.GroupId
        End Get
    End Property

    Public Property Name As String Implements IGroup.Name
        Get
            Return m_groupData.Name
        End Get
        Set(value As String)
            m_groupData.Name = value
        End Set
    End Property

    Public Function GetDataCreator(settings As Framework.ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataCreatorWrapper(scope.Resolve(Of GroupDataSaver)(New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)), New TypedParameter(GetType(GroupData), m_groupData)))
        End Using
    End Function

    Public Function GetDataUpdater(settings As Framework.ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataUpdateWrapper(scope.Resolve(Of GroupDataSaver)(New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)), New TypedParameter(GetType(GroupData), m_groupData)))
        End Using
    End Function
End Class
