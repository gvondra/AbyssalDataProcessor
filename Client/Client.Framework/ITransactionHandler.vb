Public Interface ITransactionHandler
    Inherits ISettings

    Property DbConnection As IDbConnection
    Property DbTransaction As AbyssalDataProcessor.DataTier.Utilities.IDbTransaction
End Interface
