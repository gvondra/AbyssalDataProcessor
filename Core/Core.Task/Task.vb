Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class Task
    Implements ITask

    Private m_taskData As TaskData
    Private m_taskType As ITaskType
    Private m_container As IContainer

    Friend Sub New(ByVal taskData As TaskData)
        m_taskData = taskData
        m_container = ObjectContainer.GetContainer()
    End Sub

    Friend Sub New(ByVal taskType As ITaskType)
        m_taskData = New TaskData
        m_taskType = taskType
        m_container = ObjectContainer.GetContainer()
    End Sub

    Public ReadOnly Property TaskId As Guid Implements ITask.TaskId
        Get
            Return m_taskData.TaskId
        End Get
    End Property

    Public Sub Create(transactionHandler As ITransactionHandler) Implements ISavable.Create
        Dim creator As IDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            m_taskData.TaskTypeId = m_taskType.TaskTypeId
            creator = scope.Resolve(Of TaskDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(TaskData), m_taskData)
            )
            creator.Create()
        End Using
    End Sub

    Public Sub Update(transactionHandler As ITransactionHandler) Implements ISavable.Update
        Throw New NotImplementedException()
    End Sub
End Class
