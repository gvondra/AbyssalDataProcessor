Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class Task
    Implements ITask

    Private m_taskData As TaskData
    Private m_taskType As ITaskType
    Private m_container As IContainer
    Private m_owner As IUser
    Private m_userFactory As IUserFactory

    Friend Sub New(ByVal userFactory As IUserFactory, ByVal taskData As TaskData)
        m_userFactory = userFactory
        m_taskData = taskData
        m_container = ObjectContainer.GetContainer()
    End Sub

    Friend Sub New(ByVal userFactory As IUserFactory, ByVal taskType As ITaskType)
        m_userFactory = userFactory
        m_taskData = New TaskData
        m_taskType = taskType
        m_container = ObjectContainer.GetContainer()
    End Sub

    Public ReadOnly Property TaskId As Guid Implements ITask.TaskId
        Get
            Return m_taskData.TaskId
        End Get
    End Property

    Public Property Message As String Implements ITask.Message
        Get
            Return m_taskData.Message
        End Get
        Set(value As String)
            m_taskData.Message = value
        End Set
    End Property

    Private Property UserId As Guid?
        Get
            Return m_taskData.UserId
        End Get
        Set(value As Guid?)
            m_taskData.UserId = value
        End Set
    End Property

    Public Sub Create(transactionHandler As ITransactionHandler) Implements ISavable.Create
        Dim creator As IDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            m_taskData.TaskTypeId = m_taskType.TaskTypeId
            If m_owner IsNot Nothing Then
                UserId = m_owner.UserId
            End If
            creator = scope.Resolve(Of TaskDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(TaskData), m_taskData)
            )
            creator.Create()
        End Using
    End Sub

    Public Sub Update(transactionHandler As ITransactionHandler) Implements ISavable.Update
        Dim updater As IDataUpdater
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            If m_owner IsNot Nothing Then
                UserId = m_owner.UserId
            End If
            updater = scope.Resolve(Of TaskDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(TaskData), m_taskData)
            )
            updater.Update()
        End Using
    End Sub

    Public Function GetUser(settings As ISettings) As IUser Implements ITask.GetUser
        If m_owner Is Nothing AndAlso UserId.HasValue AndAlso UserId.Value.Equals(Guid.Empty) = False Then
            m_owner = m_userFactory.Get(settings, UserId.Value)
        End If

        Return m_owner
    End Function

    Public Sub SetUser(user As IUser) Implements ITask.SetUser
        m_owner = user
    End Sub
End Class
