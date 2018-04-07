﻿Imports AbyssalDataProcessor.DataTier.Client
Imports AbyssalDataProcessor.DataTier.Client.Models
Imports Autofac
Public Class User
    Implements IUser

    Private m_userData As UserData
    Private m_container As IContainer

    Friend Sub New(ByVal organizationId As Guid)
        m_container = ObjectContainer.GetContainer()
        m_userData = New UserData() With {.OrganizationId = organizationId}
    End Sub

    Friend Sub New(ByVal userData As UserData)
        m_container = ObjectContainer.GetContainer()
        m_userData = userData
    End Sub

    Friend Sub New(ByVal container As IContainer, ByVal userData As UserData)
        m_container = container
        m_userData = userData
    End Sub

    Public ReadOnly Property OrganizationId As Guid Implements IUser.OrganizationId
        Get
            Return m_userData.OrganizationId
        End Get
    End Property

    Public Property BirthDate As DateTime? Implements IUser.BirthDate
        Get
            Return m_userData.BirthDate
        End Get
        Set(value As DateTime?)
            m_userData.BirthDate = value
        End Set
    End Property

    Public ReadOnly Property CreateTimestamp As DateTime Implements IUser.CreateTimestamp
        Get
            Return m_userData.CreateTimestamp
        End Get
    End Property

    Public Property EmailAddress As String Implements IUser.EmailAddress
        Get
            Return m_userData.EmailAddress
        End Get
        Set(value As String)
            m_userData.EmailAddress = value
        End Set
    End Property

    Public Property FullName As String Implements IUser.FullName
        Get
            Return m_userData.FullName
        End Get
        Set(value As String)
            m_userData.FullName = value
        End Set
    End Property

    Public Property PhoneNumber As String Implements IUser.PhoneNumber
        Get
            Return StringFormatters.UnformatPhoneNumber(m_userData.PhoneNumber)
        End Get
        Set(value As String)
            m_userData.PhoneNumber = StringFormatters.UnformatPhoneNumber(value)
        End Set
    End Property

    Public Property ShortName As String Implements IUser.ShortName
        Get
            Return m_userData.ShortName
        End Get
        Set(value As String)
            m_userData.ShortName = value
        End Set
    End Property

    Public ReadOnly Property UpdateTimestamp As DateTime Implements IUser.UpdateTimestamp
        Get
            Return m_userData.UpdateTimestamp
        End Get
    End Property

    Public ReadOnly Property UserId As Guid Implements IUser.UserId
        Get
            Return m_userData.UserId
        End Get
    End Property

    Public Sub CreateAccount(ByVal transactionHandler As ITransactionHandler, ByVal subscriberId As String) Implements IUser.CreateAccount
        Dim data As New UserAccountData() With {.OrganizationId = Me.OrganizationId, .UserId = Me.UserId, .SubscriberId = subscriberId}
        Dim creater As DataTier.Utilities.IDataCreator

        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            creater = scope.ResolveKeyed(Of AbyssalDataProcessor.DataTier.Utilities.IDataCreator)("UserAccountDataSaver",
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(UserAccountData), data)
            )
            creater.Create()
        End Using
    End Sub

    Public Sub Create(transactionHandler As ITransactionHandler) Implements ISavable.Create
        Dim creator As DataTier.Utilities.IDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            creator = scope.ResolveKeyed(Of AbyssalDataProcessor.DataTier.Utilities.IDataCreator)("UserDataSaver",
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(UserData), m_userData)
            )
            creator.Create()
        End Using
    End Sub

    Public Sub Update(transactionHandler As ITransactionHandler) Implements ISavable.Update
        Dim updater As DataTier.Utilities.IDataUpdater
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            updater = scope.ResolveKeyed(Of AbyssalDataProcessor.DataTier.Utilities.IDataUpdater)("UserDataSaver",
                New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ITransactionHandler), New TransactionHandler(transactionHandler)),
                New TypedParameter(GetType(UserData), m_userData)
            )
            updater.Update()
        End Using
    End Sub
End Class