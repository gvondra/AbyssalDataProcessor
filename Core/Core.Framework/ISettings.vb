﻿Public Interface ISettings
    ReadOnly Property ConnectionString As String
    Property DbConnection As IDbConnection
    Property DbTransaction As IDbTransaction
    ReadOnly Property AuthEndpointDomain As String
End Interface
