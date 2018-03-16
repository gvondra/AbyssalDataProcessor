﻿Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports autofac
Public Class Form
    Implements IForm

    Private m_formData As FormData
    Private m_user As IUser
    Private m_container As IContainer

    Friend Sub New(ByVal user As IUser,
                   ByVal formType As enumFormType,
                   ByVal formstyle As enumFormStyle,
                   ByVal content As XmlNode)

        m_container = ObjectContainer.GetContainer
        m_user = user
        m_formData = New FormData() With {
            .FormTypeId = CType(formType, Int16),
            .Style = CType(formstyle, Int16),
            .Content = content
        }
    End Sub

    Friend Sub New(ByVal container As IContainer,
                   ByVal user As IUser,
                   ByVal formType As enumFormType,
                   ByVal formstyle As enumFormStyle,
                   ByVal content As XmlNode)

        m_container = container
        m_user = user
        m_formData = New FormData() With {
            .FormTypeId = CType(formType, Int16),
            .Style = CType(formstyle, Int16),
            .Content = content
        }
    End Sub

    Public ReadOnly Property Content As XmlNode Implements IForm.Content
        Get
            Return m_formData.Content
        End Get
    End Property

    Public ReadOnly Property FormId As Guid Implements IForm.FormId
        Get
            Return m_formData.FormId
        End Get
    End Property

    Public ReadOnly Property [Type] As enumFormType Implements IForm.Type
        Get
            Return CType(m_formData.FormTypeId, enumFormType)
        End Get
    End Property

    Public Property Style As enumFormStyle Implements IForm.Style
        Get
            Return CType(m_formData.Style, enumFormStyle)
        End Get
        Set(value As enumFormStyle)
            m_formData.Style = CType(value, Int16)
        End Set
    End Property

    Private Property UserId As Guid
        Get
            Return m_formData.UserId
        End Get
        Set(value As Guid)
            m_formData.UserId = value
        End Set
    End Property

    Public Function GetDataCreator(settings As Framework.ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataCreatorWrapper(Sub() Create(settings))
        End Using
    End Function

    Private Sub Create(settings As Framework.ISettings)
        Dim creator As IDataCreator

        UserId = m_user.UserId

        creator = New FormDataSaver(New Settings(settings), m_formData)
        creator.Create()
    End Sub

    Public Function GetDataUpdater(settings As Framework.ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Throw New NotImplementedException()
    End Function
End Class