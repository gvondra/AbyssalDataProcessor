Imports System.Data.Common
Public Class DbProviderFactory
    Implements IDbProviderFactory

    Private m_innerFactory As System.Data.Common.DbProviderFactory

    Public Sub New()
        m_innerFactory = DbProviderFactories.GetFactory("System.Data.SqlClient")
    End Sub

    Public Sub EstablishTransaction(settings As ISettings) Implements IDbProviderFactory.EstablishTransaction
        If settings.Connection IsNot Nothing Then
            If settings.Connection.State <> ConnectionState.Open Then
                settings.Connection.Dispose()
                settings.Connection = Nothing
            End If
        End If
        If settings.Connection Is Nothing Then
            settings.Connection = OpenConnection(settings.ConnectionString)
        End If
        If settings.Transaction Is Nothing Then
            settings.Transaction = settings.Connection.BeginTransaction
        End If
    End Sub

    Public Function CreateConnection() As IDbConnection Implements IDbProviderFactory.CreateConnection
        Return m_innerFactory.CreateConnection
    End Function

    Public Function CreateParameter() As IDbDataParameter Implements IDbProviderFactory.CreateParameter
        Return m_innerFactory.CreateParameter
    End Function

    Public Function OpenConnection(connectionString As String) As IDbConnection Implements IDbProviderFactory.OpenConnection
        Dim connection As DbConnection
        If String.IsNullOrEmpty(connectionString) = False Then
            connection = m_innerFactory.CreateConnection
            connection.ConnectionString = connectionString
            connection.Open()
        Else
            connection = Nothing
        End If
        Return connection
    End Function
End Class
