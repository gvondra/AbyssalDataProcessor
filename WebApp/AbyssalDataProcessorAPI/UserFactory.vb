Imports Autofac
Imports System.Security.Claims
Public Class UserFactory
    Implements IUserFactory

    Private m_innerUserFactory As AbyssalDataProcessor.Core.Framework.IUserFactory
    Private m_container As IContainer

    Public Sub New()
        m_container = ObjectContainer.GetContainer()
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            m_innerUserFactory = scope.Resolve(Of AbyssalDataProcessor.Core.Framework.IUserFactory)
        End Using
    End Sub

    Public Sub New(ByVal container As IContainer)
        m_container = container
        Using scope As ILifetimeScope = m_container.BeginLifetimeScope
            m_innerUserFactory = scope.Resolve(Of AbyssalDataProcessor.Core.Framework.IUserFactory)
        End Using
    End Sub

    Public Function [Get](principal As ClaimsPrincipal) As IUser Implements IUserFactory.Get
        Dim id As Claim = principal.Claims.FirstOrDefault(Function(c As Claim) c.Type = ClaimTypes.NameIdentifier)
        Dim user As IUser = Nothing

        If id IsNot Nothing Then
            user = GetBySubscriberId(New Settings(), id.Value)
        End If

        Return user
    End Function

    Public Function GetBySubscriberId(settings As ISettings, subscriberId As String) As IUser Implements AbyssalDataProcessor.Core.Framework.IUserFactory.GetBySubscriberId
        Return m_innerUserFactory.GetBySubscriberId(settings, subscriberId)
    End Function
End Class
