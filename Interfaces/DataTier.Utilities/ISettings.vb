Public Interface ISettings
    Property Connection As IDbConnection
    Property Transaction As IDbTransaction
    ReadOnly Property ConnectionString As String
End Interface
