Public Interface IDataManagedState(Of T)
    Inherits ICloneable

    Property DataStateManager As IDataStateManager(Of T)

    Sub AcceptChanges()
End Interface
