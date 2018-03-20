Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class EventTask
    Implements IEventTask

    Private m_eventTaskData As EventTaskData
    Private m_innerTask As ITask
    Private m_event As IEvent
    Private m_container As IContainer

    Friend Sub New(ByVal [event] As IEvent, ByVal task As ITask, ByVal data As EventTaskData)
        m_eventTaskData = data
        m_innerTask = task
        m_event = [event]
        m_container = ObjectContainer.GetContainer
    End Sub

    Public ReadOnly Property TaskId As Guid Implements ITask.TaskId
        Get
            Return m_innerTask.TaskId
        End Get
    End Property

    Public Property Message As String Implements ITask.Message
        Get
            Return m_innerTask.Message
        End Get
        Set(value As String)
            m_innerTask.Message = value
        End Set
    End Property

    Public Sub Create(transactionHandler As ITransactionHandler) Implements ISavable.Create
        Dim creator As IDataCreator
        m_innerTask.Create(transactionHandler)
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            m_eventTaskData.EventId = m_event.EventId
            m_eventTaskData.TaskId = m_innerTask.TaskId
            creator = scope.Resolve(Of EventTaskDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(EventTaskData), m_eventTaskData)
            )
            creator.Create()
        End Using
    End Sub

    Public Sub Update(transactionHandler As ITransactionHandler) Implements ISavable.Update
        Throw New NotImplementedException()
    End Sub
End Class
