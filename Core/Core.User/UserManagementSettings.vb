Imports AbyssalDataProcessor.Interface.UserManagement
Public Class UserManagementSettings
    Implements [Interface].UserManagement.ISettings

    Private m_innerSettings As Framework.ISettings

    Friend Sub New(ByVal settings As Framework.ISettings)
        m_innerSettings = settings
    End Sub

    Public ReadOnly Property EndpointDomain As String Implements ISettings.EndpointDomain
        Get
            Return m_innerSettings.AuthEndpointDomain
        End Get
    End Property
End Class
