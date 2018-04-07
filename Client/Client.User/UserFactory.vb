Imports AbyssalDataProcessor.DataTier.Client
Imports AbyssalDataProcessor.DataTier.Client.Models
Imports Autofac
Public Class UserFactory
    Implements IUserFactory

    Private m_container As IContainer

    Public Sub New()
        m_container = ObjectContainer.GetContainer()
    End Sub

    Public Sub New(ByVal container As IContainer)
        m_container = container
    End Sub

    Public Function Create() As IUser Implements IUserFactory.Create
        Return New User(New UserData)
    End Function

    Public Function [Get](settings As ISettings, ByVal organizationId As Guid, userId As Guid) As IUser Implements IUserFactory.Get
        Dim userDataFactory As IUserDataFactory
        Dim data As UserData
        Dim result As User = Nothing
        Using scope = m_container.BeginLifetimeScope()
            userDataFactory = scope.Resolve(Of IUserDataFactory)
            data = userDataFactory.Get(New Settings(settings), organizationId, userId)
            If data IsNot Nothing Then
                result = New User(data)
            End If
        End Using
        Return result
    End Function

    Public Function GetBySubscriberId(settings As ISettings, ByVal organizationId As Guid, subscriberId As String) As IUser Implements IUserFactory.GetBySubscriberId
        Dim userDataFactory As IUserDataFactory
        Dim data As UserData
        Dim result As User = Nothing
        Using scope = m_container.BeginLifetimeScope()
            userDataFactory = scope.Resolve(Of IUserDataFactory)
            data = userDataFactory.GetBySubscriberId(New Settings(settings), organizationId, subscriberId)
            If data IsNot Nothing Then
                result = New User(data)
            End If
        End Using
        Return result
    End Function

    Public Function Search(settings As ISettings, ByVal organizationId As Guid, searchText As String) As IEnumerable(Of IUser) Implements IUserFactory.Search
        Dim userDataFactory As IUserDataFactory
        Dim result As New List(Of IUser)
        Using scope = m_container.BeginLifetimeScope()
            userDataFactory = scope.Resolve(Of IUserDataFactory)
            For Each data As UserData In userDataFactory.Search(New Settings(settings), organizationId, searchText)
                result.Add(New User(data))
            Next
        End Using
        Return result
    End Function
End Class
