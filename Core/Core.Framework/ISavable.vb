Public Interface ISavable
    Function GetDataCreator(ByVal settings As ISettings) As IDataCreator
    Function GetDataUpdater(ByVal settings As ISettings) As IDataUpdater
End Interface
