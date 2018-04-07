Public Class Settings
    Implements AbyssalDataProcessor.Core.Utilities.Framework.ISettings

    Public ReadOnly Property AuthEndpointDomain As String Implements AbyssalDataProcessor.Core.Utilities.Framework.ISettings.AuthEndpointDomain
        Get
            Return My.Settings.Auth0Domain
        End Get
    End Property

    Public ReadOnly Property ConnectionString As String Implements AbyssalDataProcessor.Core.Utilities.Framework.ISettings.ConnectionString
        Get
            Return ConfigurationManager.ConnectionStrings("adp").ConnectionString
        End Get
    End Property
End Class
