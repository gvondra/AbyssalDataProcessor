﻿Public Class TaskData
    Implements IDataManagedState(Of TaskData)

    <ColumnMapping("TaskId")> Public Property TaskId As Guid
    <ColumnMapping("TaskTypeId")> Public Property TaskTypeId As Guid
    <ColumnMapping("CreateTimestamp")> Public Property CreateTimestamp As Date
    <ColumnMapping("UpdateTimestamp")> Public Property UpdateTimestamp As Date

    Public Property DataStateManager As IDataStateManager(Of TaskData) = New DataStateManager(Of TaskData) Implements IDataManagedState(Of TaskData).DataStateManager

    Public Sub AcceptChanges() Implements IDataManagedState(Of TaskData).AcceptChanges
        DataStateManager.Original = CType(Clone(), TaskData)
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Return MemberwiseClone()
    End Function
End Class