Public Interface ITransactionHandler
    Inherits ISettings

    Property DbConnection As IDbConnection
    Property DbTransaction As IDbTransaction
End Interface
