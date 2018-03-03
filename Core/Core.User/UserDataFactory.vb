Imports AbyssalDataProcessor.DataTier.Core
Imports AbyssalDataProcessor.DataTier.Core.Models
Imports Autofac
Public Class UserDataFactory
    Implements IUserFactory

    Private m_container As IContainer

    Public Sub New()
        m_container = ObjectContainer.GetContainer()
    End Sub

    Public Sub New(ByVal container As IContainer)
        m_container = container
    End Sub

    Public Function GetBySubscriberId(settings As ISettings, subscriberId As String) As IUser Implements IUserFactory.GetBySubscriberId
        Dim userDataFactory As IUserDataFactory
        Dim data As UserData
        Dim result As User = Nothing
        Using scope = m_container.BeginLifetimeScope()
            userDataFactory = scope.Resolve(Of IUserDataFactory)
            data = userDataFactory.GetBySubscriberId(New Settings(settings), subscriberId)
            If data IsNot Nothing Then
                result = New User(data)
            End If
        End Using
        Return result
    End Function
End Class
