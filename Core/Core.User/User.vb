Imports AbyssalDataProcessor.DataTier.Core.Models
Public Class User
    Implements IUser

    Private m_userData As UserData

    Friend Sub New(ByVal userData As UserData)
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
End Class
