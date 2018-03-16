Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Imports System.Xml

Public Class EventForm
    Implements IForm

    Private m_eventFormData As EventFormData
    Private m_innerForm As IForm
    Private m_containter As IContainer

    Public Property [Event] As IEvent

    Friend Sub New(ByVal [event] As IEvent, ByVal form As IForm)
        m_eventFormData = New EventFormData
        Me.Event = [event]
        m_innerForm = form
        m_containter = ObjectContainer.GetContainer
    End Sub

    Public ReadOnly Property Content As XmlNode Implements IForm.Content
        Get
            Return m_innerForm.Content
        End Get
    End Property

    Public ReadOnly Property FormId As Guid Implements IForm.FormId
        Get
            Return m_innerForm.FormId
        End Get
    End Property

    Public Property Style As enumFormStyle Implements IForm.Style
        Get
            Return m_innerForm.Style
        End Get
        Set(value As enumFormStyle)
            m_innerForm.Style = value
        End Set
    End Property

    Public ReadOnly Property Type As enumFormType Implements IForm.Type
        Get
            Return m_innerForm.Type
        End Get
    End Property

    Public Function GetDataCreator(settings As Framework.ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Dim innerCreator As Framework.IDataCreator = m_innerForm.GetDataCreator(settings)
        Return New DataCreatorWrapper(Sub() Create(settings, innerCreator))
    End Function

    Private Sub Create(settings As Framework.ISettings, innerCreator As Framework.IDataCreator)
        Dim creator As IDataCreator

        innerCreator.Create()

        m_eventFormData.EventId = Me.Event.EventId
        m_eventFormData.FormId = m_innerForm.FormId
        Using scope As ILifetimeScope = m_containter.BeginLifetimeScope
            creator = scope.Resolve(Of EventFormDataSaver)(New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)), New TypedParameter(GetType(EventFormData), m_eventFormData))
            creator.Create()
        End Using
    End Sub

    Public Function GetDataUpdater(settings As Framework.ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Return m_innerForm.GetDataUpdater(settings)
    End Function
End Class
