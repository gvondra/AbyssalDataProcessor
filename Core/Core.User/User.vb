Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class User
    Implements IUser

    Private m_userData As UserData
    Private m_container As IContainer

    Friend Sub New(ByVal userData As UserData)
        m_container = ObjectContainer.GetContainer()
        m_userData = userData
    End Sub

    Friend Sub New(ByVal container As IContainer, ByVal userData As UserData)
        m_container = container
        m_userData = userData
    End Sub

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
            Return m_userData.PhoneNumber
        End Get
        Set(value As String)
            m_userData.PhoneNumber = value
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

    Public Function GetAccountDataCreater(ByVal settings As Framework.ISettings, ByVal subscriberId As String) As Framework.IDataCreator Implements IUser.GetAccountDataCreater
        Return New DataCreatorWrapper(Sub() CreateAccount(settings, subscriberId))
    End Function

    Private Sub CreateAccount(ByVal settings As Framework.ISettings, ByVal subscriberId As String)
        Dim data As New UserAccountData() With {.UserId = Me.UserId, .SubscriberId = subscriberId}
        Dim creater As IDataCreator
        Dim innerSettings As New Settings(settings)

        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            creater = scope.Resolve(Of UserAccountDataSaver)(New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), innerSettings), New TypedParameter(GetType(UserAccountData), data))
            creater.Create()
        End Using

    End Sub

    Public Function GetDataCreator(settings As ISettings) As Framework.IDataCreator Implements ISavable.GetDataCreator
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataCreatorWrapper(scope.Resolve(Of UserDataSaver)(New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)), New TypedParameter(GetType(UserData), m_userData)))
        End Using
    End Function

    Public Function GetDataUpdater(settings As ISettings) As Framework.IDataUpdater Implements ISavable.GetDataUpdater
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            Return New DataUpdateWrapper(scope.Resolve(Of UserDataSaver)(New TypedParameter(GetType(AbyssalDataProcessor.DataTier.Utilities.ISettings), New Settings(settings)), New TypedParameter(GetType(UserData), m_userData)))
        End Using
    End Function
End Class
