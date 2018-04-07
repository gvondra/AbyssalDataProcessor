Imports AbyssalDataProcessor.DataTier.Client
Imports AbyssalDataProcessor.DataTier.Client.Models
Imports Autofac
Public Class Form
    Implements IForm

    Private m_formData As FormData
    Private m_user As IUser
    Private m_container As IContainer

    Friend Sub New(ByVal organziationId As Guid,
                    ByVal user As IUser,
                    ByVal type As Int16,
                    ByVal content As String)

        m_formData = New FormData() With {.OrganizationId = organziationId, .FormTypeId = type, .Content = content}
        m_user = user
        m_container = ObjectContainer.GetContainer
    End Sub

    Public ReadOnly Property Content As String Implements IForm.Content
        Get
            Return m_formData.Content
        End Get
    End Property

    Public ReadOnly Property CreateTimestamp As DateTime Implements IForm.CreateTimestamp
        Get
            Return m_formData.CreateTimestamp
        End Get
    End Property

    Public ReadOnly Property FormId As Guid Implements IForm.FormId
        Get
            Return m_formData.FormId
        End Get
    End Property

    Public ReadOnly Property OrganizationId As Guid Implements IForm.OrganizationId
        Get
            Return m_formData.OrganizationId
        End Get
    End Property

    Public Property Style As Int16 Implements IForm.Style
        Get
            Return m_formData.Style
        End Get
        Set(value As Int16)
            m_formData.Style = value
        End Set
    End Property

    Public ReadOnly Property Type As Int16 Implements IForm.Type
        Get
            Return m_formData.FormTypeId
        End Get
    End Property

    Public Sub Create(transactionHandler As ITransactionHandler) Implements ISavable.Create
        Dim creator As DataTier.Utilities.IDataCreator

        m_formData.UserId = m_user.UserId

        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            creator = scope.ResolveKeyed(Of DataTier.Utilities.IDataCreator)("FormDataSaver",
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(FormData), m_formData)
            )
        End Using

        creator.Create()
    End Sub

    Public Sub Update(transactionHandler As ITransactionHandler) Implements ISavable.Update
        Dim updater As DataTier.Utilities.IDataUpdater

        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            updater = scope.ResolveKeyed(Of DataTier.Utilities.IDataUpdater)("FormDataSaver",
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(FormData), m_formData)
            )
        End Using

        updater.Update()
    End Sub
End Class
