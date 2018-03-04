﻿Public Interface IDbProviderFactory
    Function CreateConnection() As IDbConnection
    Function CreateParameter() As IDbDataParameter
    Function OpenConnection(ByVal connectionString As String) As IDbConnection
    Sub EstablishTransaction(ByVal settings As ISettings)
End Interface