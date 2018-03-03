Imports AbyssalDataProcessor.DataTier.Utilities

Public Class Settings
    Implements AbyssalDataProcessor.DataTier.Utilities.ISettings

    Private m_innerSettings As Framework.ISettings

    Public Sub New(ByVal settings As Framework.ISettings)
        m_innerSettings = settings
    End Sub

    Public Property Connection As IDbConnection Implements ISettings.Connection
    Public Property Transaction As IDbTransaction Implements ISettings.Transaction

    Public ReadOnly Property ConnectionString As String Implements ISettings.ConnectionString
        Get
            Return m_innerSettings.ConnectionString
        End Get
    End Property

End Class
