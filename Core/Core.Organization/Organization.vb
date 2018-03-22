Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class Organization
    Implements IOrganization

    Private m_organizationData As OrganizationData
    Private m_container As IContainer

    Friend Sub New()
        m_container = ObjectContainer.GetContainer
        m_organizationData = New OrganizationData
    End Sub

    Friend Sub New(ByVal organizationData As OrganizationData)
        m_container = ObjectContainer.GetContainer
        m_organizationData = organizationData
    End Sub

    Public Property Name As String Implements IOrganization.Name
        Get
            Return m_organizationData.Name
        End Get
        Set(value As String)
            m_organizationData.Name = value
        End Set
    End Property

    Public ReadOnly Property OrganizationId As Guid Implements IOrganization.OrganizationId
        Get
            Return m_organizationData.OrganizationId
        End Get
    End Property

    Public Sub Create(transactionHandler As ITransactionHandler) Implements ISavable.Create
        Dim creator As IDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            creator = scope.ResolveKeyed(Of IDataCreator)("OrganizationDataSaver",
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(OrganizationData), m_organizationData)
            )
            creator.Create()
        End Using
    End Sub

    Public Sub Update(transactionHandler As ITransactionHandler) Implements ISavable.Update
        Dim updater As IDataUpdater
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            updater = scope.ResolveKeyed(Of IDataUpdater)("OrganizationDataSaver",
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(OrganizationData), m_organizationData)
            )
            updater.Update()
        End Using
    End Sub
End Class
