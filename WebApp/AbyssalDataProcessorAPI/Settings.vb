Public Class Settings
    Implements AbyssalDataProcessor.Core.Framework.ISettings

    Public ReadOnly Property ConnectionString As String Implements ISettings.ConnectionString
        Get
            Return ConfigurationManager.ConnectionStrings("adp").ConnectionString
        End Get
    End Property

    Public Property DbConnection As IDbConnection Implements ISettings.DbConnection
    Public Property DbTransaction As IDbTransaction Implements ISettings.DbTransaction
End Class
