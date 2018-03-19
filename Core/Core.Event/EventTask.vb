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

    Public Function GetDataCreator(settings As ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Return New DataCreatorWrapper(Sub() Create(settings))
    End Function

    Private Sub Create(ByVal settings As ISettings)
        Dim creator As IDataCreator

        m_innerTask.GetDataCreator(settings).Create()

        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            m_eventTaskData.EventId = m_event.EventId
            m_eventTaskData.TaskId = m_innerTask.TaskId
            creator = scope.Resolve(Of EventTaskDataSaver)(
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)),
                New TypedParameter(GetType(EventTaskData), m_eventTaskData)
            )
            creator.Create()
        End Using
    End Sub

    Public Function GetDataUpdater(settings As ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Throw New NotImplementedException()
    End Function
End Class
